using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SchoolService.Application.Abstractions;
using SchoolService.Application.Services;
using SchoolService.Infrastructure.Persistence;
using SchoolService.Infrastructure.Repositories;
using SchoolService.Infrastructure.Services;
using System.Security.Claims;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var issuer = builder.Configuration["CognitoJwtConfig:Issuer"]
    ?? throw new InvalidOperationException("CognitoJwtConfig:Issuer is not configured.");
var expectedClientId = builder.Configuration["CognitoJwtConfig:Audience"]
    ?? throw new InvalidOperationException("CognitoJwtConfig:Audience is not configured.");
var identityServiceBaseUrl = builder.Configuration["IdentityService:BaseUrl"]
    ?? throw new InvalidOperationException("IdentityService:BaseUrl is not configured.");

// Add services to the container.

builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("SchoolDb"),
        npgsqlOptions => npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "infra"))
        .UseSnakeCaseNamingConvention());

var dataProtection = builder.Services.AddDataProtection()
    .SetApplicationName("SchoolXP");
var dataProtectionKeysPath = builder.Configuration["DataProtection:KeysPath"];
if (!string.IsNullOrWhiteSpace(dataProtectionKeysPath))
{
    dataProtection.PersistKeysToFileSystem(new DirectoryInfo(dataProtectionKeysPath));
}
else if (builder.Environment.IsProduction())
{
    throw new InvalidOperationException("DataProtection:KeysPath must point to a persistent protected key store in production.");
}

builder.Services.AddHttpClient("IdentityService", client =>
{
    client.BaseAddress = new Uri(identityServiceBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(5);
});

builder.Services.AddHttpClient<IIdentityUserClient, IdentityUserClient>(client =>
{
    client.BaseAddress = new Uri(identityServiceBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(5);
});
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRegistrationRepository, StudentRegistrationRepository>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IStaffRegistrationRepository, StaffRegistrationRepository>();
builder.Services.AddSingleton<IIdentityDocumentProtector, DataProtectionIdentityDocumentProtector>();
builder.Services.AddScoped<ISchoolMembershipRepository, SchoolMembershipRepository>();
builder.Services.AddScoped<ISchoolRegistrationRepository, SchoolRegistrationRepository>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = issuer;
        options.MapInboundClaims = false;
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateAudience = false,
            ClockSkew = TimeSpan.FromMinutes(2)
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                if (context.SecurityToken is not JsonWebToken token)
                {
                    context.Fail("Unsupported token format.");
                    return;
                }

                var clientId = token.Claims.FirstOrDefault(claim => claim.Type == "client_id")?.Value;
                var tokenUse = token.Claims.FirstOrDefault(claim => claim.Type == "token_use")?.Value;
                if (tokenUse != "access" || clientId != expectedClientId)
                {
                    context.Fail("Invalid access token.");
                    return;
                }

                var authorizationHeader = context.Request.Headers["Authorization"].ToString();
                if (string.IsNullOrWhiteSpace(authorizationHeader))
                {
                    context.Fail("Missing bearer token.");
                    return;
                }

                try
                {
                    var httpClient = context.HttpContext.RequestServices
                        .GetRequiredService<IHttpClientFactory>()
                        .CreateClient("IdentityService");
                    using var request = new HttpRequestMessage(HttpMethod.Get, "api/Auth");
                    request.Headers.TryAddWithoutValidation("Authorization", authorizationHeader);

                    using var response = await httpClient.SendAsync(
                        request,
                        context.HttpContext.RequestAborted);
                    if (!response.IsSuccessStatusCode)
                    {
                        context.Fail("IdentityService could not resolve the authenticated user.");
                        return;
                    }

                    var user = await response.Content.ReadFromJsonAsync<IdentityUserResponse>(
                        cancellationToken: context.HttpContext.RequestAborted);
                    if (user is null || user.Id == Guid.Empty || string.IsNullOrWhiteSpace(user.Email))
                    {
                        context.Fail("IdentityService returned an invalid user response.");
                        return;
                    }

                    var identity = context.Principal?.Identity as ClaimsIdentity;
                    if (identity is null)
                    {
                        context.Fail("Could not attach user claims.");
                        return;
                    }

                    RemoveClaims(identity, "user_id", "email", "school_role", "school_id", "school-id", "is_platform_admin");
                    identity.AddClaim(new Claim("user_id", user.Id.ToString()));
                    identity.AddClaim(new Claim("email", user.Email));
                    if (user.IsPlatformAdmin)
                    {
                        identity.AddClaim(new Claim("is_platform_admin", "true"));
                    }

                    var memberships = await context.HttpContext.RequestServices
                        .GetRequiredService<ISchoolMembershipRepository>()
                        .GetActiveRolesForUserAsync(user.Id, context.HttpContext.RequestAborted);

                    var isPlatformAdmin = user.IsPlatformAdmin;
                    var hasSchoolHeader = context.Request.Headers.ContainsKey("X-School-Id");
                    Guid? selectedSchoolId = null;
                    if (hasSchoolHeader)
                    {
                        var schoolHeader = context.Request.Headers["X-School-Id"].ToString();
                        if (!Guid.TryParse(schoolHeader, out var parsedSchoolId))
                        {
                            context.Fail("The selected school is not actively linked to the authenticated user.");
                            return;
                        }

                        if (isPlatformAdmin)
                        {
                            var selectedSchool = await context.HttpContext.RequestServices
                                .GetRequiredService<ISchoolMembershipRepository>()
                                .GetAllSchoolsAsync(context.HttpContext.RequestAborted);
                            if (!selectedSchool.Any(school =>
                                school.SchoolId == parsedSchoolId && school.IsActive))
                            {
                                context.Fail("The selected school is not active.");
                                return;
                            }
                        }
                        else if (!memberships.Any(membership => membership.SchoolId == parsedSchoolId))
                        {
                            context.Fail("The selected school is not actively linked to the authenticated user.");
                            return;
                        }

                        selectedSchoolId = parsedSchoolId;
                        identity.AddClaim(new Claim("school-id", parsedSchoolId.ToString()));
                        identity.AddClaim(new Claim("school_id", parsedSchoolId.ToString()));
                    }

                    if (isPlatformAdmin && selectedSchoolId.HasValue)
                    {
                        var roleClaim = new Claim("school_role", "platformadmin");
                        roleClaim.Properties["school_id"] = selectedSchoolId.Value.ToString();
                        identity.AddClaim(roleClaim);
                    }

                    foreach (var membership in memberships)
                    {
                        if (selectedSchoolId.HasValue && membership.SchoolId != selectedSchoolId.Value)
                        {
                            continue;
                        }

                        var roleClaim = new Claim("school_role", membership.Role);
                        roleClaim.Properties["school_id"] = membership.SchoolId.ToString();
                        identity.AddClaim(roleClaim);
                    }
                }
                catch (Exception exception) when (
                    exception is not OperationCanceledException ||
                    !context.HttpContext.RequestAborted.IsCancellationRequested)
                {
                    context.Fail("User roles could not be resolved.");
                }
            }
        };
    });

builder.Services.AddAuthorization();
 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("local-angular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("local-angular");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static void RemoveClaims(ClaimsIdentity identity, params string[] claimTypes)
{
    foreach (var claim in identity.Claims.Where(claim => claimTypes.Contains(claim.Type)).ToArray())
    {
        identity.RemoveClaim(claim);
    }
}

internal sealed record IdentityUserResponse(Guid Id, string Email, bool IsPlatformAdmin);

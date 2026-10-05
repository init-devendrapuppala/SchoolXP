using IdentityService.Application.Abstractions;
using IdentityService.Application.Authentication;
using IdentityService.Infrastructure.Persistence;
using IdentityService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Show detailed IdentityModel errors only in Development
if (builder.Environment.IsDevelopment())
{
    IdentityModelEventSource.ShowPII = true;
}

// Configuration
var issuer = builder.Configuration["CognitoJwtConfig:Issuer"];
var expectedClientId = builder.Configuration["CognitoJwtConfig:Audience"];

builder.Services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityDb")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IIdentityUserService, IdentityUserService>();
builder.Services.AddSingleton<ITokenService>(_ => new TokenService(
    issuer ?? throw new InvalidOperationException("CognitoJwtConfig:Issuer is not configured."),
    expectedClientId ?? throw new InvalidOperationException("CognitoJwtConfig:Audience is not configured.")));

// Authentication
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
            // Cognito access tokens use client_id instead of aud
            ValidateAudience = false,

            ClockSkew = TimeSpan.FromMinutes(2)
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                if (context.SecurityToken is JsonWebToken token)
                {
                    var clientId = token.Claims.FirstOrDefault(c => c.Type == "client_id")?.Value;
                    var tokenUse = token.Claims.FirstOrDefault(c => c.Type == "token_use")?.Value;

                    if (tokenUse != "access")
                    {
                        context.Fail("Only access tokens are accepted.");
                        return Task.CompletedTask;
                    }

                    if (clientId != expectedClientId)
                    {
                        context.Fail("Invalid Cognito App Client.");
                        return Task.CompletedTask;
                    }
                }

                return Task.CompletedTask;
            },

            OnAuthenticationFailed = context =>
            {
                Console.WriteLine(context.Exception.ToString());
                return Task.CompletedTask;
            },

            OnChallenge = context =>
            {
                Console.WriteLine($"Error: {context.Error}");
                return Task.CompletedTask;
            }
        };
    });

// Authorization
builder.Services.AddAuthorization();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Paste only the JWT access token. Do NOT prefix it with 'Bearer'."
        });

    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});

// CORS
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

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("local-angular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
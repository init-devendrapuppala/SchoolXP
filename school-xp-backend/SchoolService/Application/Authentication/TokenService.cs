using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using SchoolService.Application.Abstractions;

namespace SchoolService.Application.Authentication;

public sealed class TokenService : ITokenService
{
    private readonly string _issuer;
    private readonly string _clientId;
    private readonly ConfigurationManager<OpenIdConnectConfiguration> _configurationManager;

    public TokenService(string issuer, string clientId)
    {
        _issuer = issuer;
        _clientId = clientId;
        _configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
            $"{issuer.TrimEnd('/')}/.well-known/openid-configuration",
            new OpenIdConnectConfigurationRetriever(),
            new HttpDocumentRetriever { RequireHttps = true });
    }

    public async Task<string> GetVerifiedIdTokenEmailAsync(
        string idToken,
        string expectedSubject,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(idToken) || string.IsNullOrWhiteSpace(expectedSubject))
        {
            throw new SecurityTokenException("The ID token or authenticated subject is missing.");
        }

        var configuration = await _configurationManager.GetConfigurationAsync(ct);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _issuer,
            ValidateAudience = true,
            ValidAudience = _clientId,
            ValidateIssuerSigningKey = true,
            IssuerSigningKeys = configuration.SigningKeys,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true,
            ClockSkew = TimeSpan.FromMinutes(2),
        };

        var handler = new JsonWebTokenHandler();
        var validationResult = await handler.ValidateTokenAsync(idToken, validationParameters);
        if (!validationResult.IsValid)
        {
            throw new SecurityTokenException("The ID token failed validation.", validationResult.Exception);
        }

        if (validationResult.SecurityToken is not JsonWebToken validatedToken)
        {
            throw new SecurityTokenException("The validated ID token has an unsupported format.");
        }

        var tokenUse = validatedToken.Claims.FirstOrDefault(claim => claim.Type == "token_use")?.Value;
        var subject = validatedToken.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
        var email = validatedToken.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value;
        var emailVerified = validatedToken.Claims.FirstOrDefault(claim => claim.Type == "email_verified")?.Value;

        if (tokenUse != "id" ||
            !string.Equals(subject, expectedSubject, StringComparison.Ordinal) ||
            string.IsNullOrWhiteSpace(email) ||
            !string.Equals(emailVerified, "true", StringComparison.OrdinalIgnoreCase))
        {
            throw new SecurityTokenException(
                "The ID token claims do not match the authenticated user or do not contain a verified email.");
        }

        return email.Trim().ToLowerInvariant();
    }
}

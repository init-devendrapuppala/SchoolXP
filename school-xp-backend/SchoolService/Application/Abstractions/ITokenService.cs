namespace SchoolService.Application.Abstractions;

public interface ITokenService
{
    Task<string> GetVerifiedIdTokenEmailAsync(
        string idToken,
        string expectedSubject,
        CancellationToken ct);
}

namespace SchoolService.Application.Abstractions;

public interface IIdentityUserClient
{
    Task<Guid> CreateOrGetUserAsync(
        string email,
        string firstName,
        string lastName,
        string accessToken,
        CancellationToken ct);
}
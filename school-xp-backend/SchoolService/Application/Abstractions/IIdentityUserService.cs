using SchoolService.Domain.Entities;

namespace SchoolService.Application.Abstractions;

public interface IIdentityUserService
{
    Task<Guid> CreateOrGetUserAsync(
        string email,
        string firstName,
        string lastName,
        CancellationToken ct);

    Task<UserProfile?> FindUserByCognitoSubAsync(string sub, CancellationToken ct);

    Task<UserLinkResult> LinkCognitoSubAsync(
        string email,
        string sub,
        CancellationToken ct);

    Task<bool> DeactivateUserAsync(Guid userId, CancellationToken ct);
}

public sealed record UserLinkResult(UserLinkStatus Status, Guid? UserId);

public enum UserLinkStatus
{
    Linked,
    UserNotFound,
    UserInactive,
    CognitoSubAlreadyLinked,
    AmbiguousEmail,
}

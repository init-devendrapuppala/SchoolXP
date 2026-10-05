using IdentityService.Application.Contracts.User;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Abstractions;

public interface IUserRepository
{
    Task<UserProfile?> GetByCognitoSubAsync(
        string subKey,
        CancellationToken ct);

    Task<UserProfile?> GetByEmailAsync(
        string email,
        CancellationToken ct);

    Task<UserIdentity?> GetIdentityBySubAsync(
        string subKey,
        CancellationToken ct);

    Task AddIdentityAsync(
        UserIdentity identity,
        CancellationToken ct);

    Task<Guid> CreateUserIfNotExistsAsync(
        UserProfile user,
        CancellationToken ct
        );

    Task DeactivateUserAsync(
        Guid userId,
        CancellationToken ct
        );
}
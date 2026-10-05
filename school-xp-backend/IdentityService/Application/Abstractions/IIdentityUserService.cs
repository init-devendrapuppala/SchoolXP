using IdentityService.Application.Contracts.Authentication;
using IdentityService.Application.Contracts.User;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Abstractions
{
    public interface IIdentityUserService
    {
        public Task<UserDetails?> FindUserByCognitoSubAsync(string subKey, CancellationToken ct);
        Task<UserLinkResult> UpdateUserCognitoSubAsync(string email, string subKey, CancellationToken ct);
        Task<Guid> CreateUserAsync(CreateUserRequest request, CancellationToken ct);
        Task DeactivateUser(Guid userId, CancellationToken ct);
    }
}

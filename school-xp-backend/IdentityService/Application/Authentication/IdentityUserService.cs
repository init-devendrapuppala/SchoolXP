using IdentityService.Application.Abstractions;
using IdentityService.Application.Contracts.Authentication;
using IdentityService.Application.Contracts.User;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Authentication
{
    public class IdentityUserService(IUserRepository userRepository) : IIdentityUserService
    {
        public async Task<Guid> CreateUserAsync(CreateUserRequest request, CancellationToken ct)
        {
            var email = request.UserEmail.Trim().ToLowerInvariant();
            var user = new UserProfile
            {
                Email = email,
                FirstName = request.FirstName.Trim(),
                LastName = request.Lastname.Trim(),
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = null,
                isActive = true
            };

            return await userRepository.CreateUserIfNotExistsAsync(user, ct);
        }

        public Task DeactivateUser(Guid userId, CancellationToken ct)
        {
            return userRepository.DeactivateUserAsync(userId, ct);
        }

        public async Task<UserDetails?> FindUserByCognitoSubAsync(string subKey, CancellationToken ct)
        {
            var user = await userRepository.GetByCognitoSubAsync(subKey, ct);
            if (user is null) return null;

            return new UserDetails
            {
                Id = user.UserId,
                Email = user.Email,
                isActive = user.isActive,
                IsPlatformAdmin = user.IsPlatformAdmin,
                Sub = subKey
            };
        }

        public async Task<UserLinkResult> UpdateUserCognitoSubAsync(
    string email,
    string subKey,
    CancellationToken ct)
        {
            var user = await userRepository.GetByEmailAsync(email, ct);

            if (user is null)
            {
                return new UserLinkResult(
                    UserLinkStatus.StudentNotFound,
                    null);
            }

            if (!user.isActive)
            {
                return new UserLinkResult(UserLinkStatus.UserInactive, user.UserId);
            }

            var existingIdentity =
                await userRepository.GetIdentityBySubAsync(subKey, ct);

            if (existingIdentity is not null)
            {
                return new UserLinkResult(
                    existingIdentity.UserId == user.UserId
                        ? UserLinkStatus.Linked
                        : UserLinkStatus.CognitoSubAlreadyLinked,
                    existingIdentity.UserId);
            }

            await userRepository.AddIdentityAsync(
                new UserIdentity
                {
                    UserId = user.UserId,
                    Sub = subKey
                },
                ct);

            return new UserLinkResult(
                UserLinkStatus.Linked,
                user.UserId);
        }
    }
}

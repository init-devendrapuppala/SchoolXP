using IdentityService.Application.Abstractions;
using IdentityService.Application.Contracts.User;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Repositories;

public class UserRepository(IdentityDbContext dbContext) : IUserRepository
{
    public Task<UserProfile?> GetByCognitoSubAsync(
    string subKey,
    CancellationToken ct)
    {
        return dbContext.UserProfiles
            .Include(user => user.UserIdentities
                .Where(identity => identity.Sub == subKey))
            .SingleOrDefaultAsync(
                user => user.UserIdentities
                    .Any(identity => identity.Sub == subKey),
                ct);
    }

    public Task<UserProfile?> GetByEmailAsync(
    string email,
    CancellationToken ct)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        return dbContext.UserProfiles
            .SingleOrDefaultAsync(user => user.Email.ToLower() == normalizedEmail, ct);
    }

    public Task<UserIdentity?> GetIdentityBySubAsync(
        string subKey,
        CancellationToken ct)
    {
        return dbContext.UserIdentities
            .SingleOrDefaultAsync(identity => identity.Sub == subKey, ct);
    }

    public async Task AddIdentityAsync(
        UserIdentity identity,
        CancellationToken ct)
    {
        await dbContext.UserIdentities.AddAsync(identity, ct);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task<Guid> CreateUserIfNotExistsAsync(UserProfile user, CancellationToken ct)
    {
        var existingUser = await GetByEmailAsync(user.Email, ct);
        if (existingUser is not null)
        {
            return existingUser.UserId;
        }

        await dbContext.UserProfiles.AddAsync(user, ct);
        try
        {
            await dbContext.SaveChangesAsync(ct);
            return user.UserId;
        }
        catch (DbUpdateException)
        {
            dbContext.Entry(user).State = EntityState.Detached;
            existingUser = await GetByEmailAsync(user.Email, ct);
            if (existingUser is null)
            {
                throw;
            }

            return existingUser.UserId;
        }
    }

    public async Task DeactivateUserAsync(Guid userId, CancellationToken ct)
    {
        var user = await dbContext.UserProfiles.SingleOrDefaultAsync(u => u.UserId == userId, ct);
        user.isActive = false;
        user.UpdatedAtUtc = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(ct);
        return;
    }
}

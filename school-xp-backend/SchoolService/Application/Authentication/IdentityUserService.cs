using Microsoft.EntityFrameworkCore;
using SchoolService.Application.Abstractions;
using SchoolService.Domain.Entities;
using SchoolService.Infrastructure.Persistence;

namespace SchoolService.Application.Authentication;

public sealed class IdentityUserService(SchoolDbContext dbContext) : IIdentityUserService
{
    public async Task<Guid> CreateOrGetUserAsync(
        string email,
        string firstName,
        string lastName,
        CancellationToken ct)
    {
        var normalizedEmail = NormalizeEmail(email);
        var users = await dbContext.UserProfiles
            .Include(user => user.UserIdentities)
            .Where(user => user.IsAccount && user.Email == normalizedEmail)
            .ToListAsync(ct);
        if (users.Count == 1)
        {
            return users[0].UserId;
        }
        if (users.Count > 1)
        {
            var linkedUsers = users.Where(user => user.UserIdentities.Count != 0).ToArray();
            if (linkedUsers.Length == 1)
            {
                return linkedUsers[0].UserId;
            }

            throw new AmbiguousUserEmailException();
        }

        var user = new UserProfile
        {
            Email = normalizedEmail,
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            IsAccount = true,
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow,
        };

        await dbContext.UserProfiles.AddAsync(user, ct);
        try
        {
            await dbContext.SaveChangesAsync(ct);
            return user.UserId;
        }
        catch (DbUpdateException)
        {
            dbContext.Entry(user).State = EntityState.Detached;
            users = await dbContext.UserProfiles
                .Include(candidate => candidate.UserIdentities)
                .Where(candidate => candidate.IsAccount && candidate.Email == normalizedEmail)
                .ToListAsync(ct);
            if (users.Count == 0)
            {
                throw;
            }
            if (users.Count > 1)
            {
                var linkedUsers = users.Where(candidate => candidate.UserIdentities.Count != 0).ToArray();
                if (linkedUsers.Length != 1)
                {
                    throw new AmbiguousUserEmailException();
                }

                return linkedUsers[0].UserId;
            }

            return users[0].UserId;
        }
    }

    public Task<UserProfile?> FindUserByCognitoSubAsync(string sub, CancellationToken ct)
    {
        return dbContext.UserProfiles
            .Include(user => user.UserIdentities.Where(identity => identity.Sub == sub))
            .SingleOrDefaultAsync(
                user => user.UserIdentities.Any(identity => identity.Sub == sub),
                ct);
    }

    public async Task<UserLinkResult> LinkCognitoSubAsync(
        string email,
        string sub,
        CancellationToken ct)
    {
        var users = await dbContext.UserProfiles
            .Where(profile => profile.IsAccount && profile.Email == NormalizeEmail(email))
            .ToListAsync(ct);
        if (users.Count == 0)
        {
            return new UserLinkResult(UserLinkStatus.UserNotFound, null);
        }
        if (users.Count > 1)
        {
            return new UserLinkResult(UserLinkStatus.AmbiguousEmail, null);
        }
        var user = users[0];

        if (!user.IsActive)
        {
            return new UserLinkResult(UserLinkStatus.UserInactive, user.UserId);
        }

        var existingIdentity = await dbContext.UserIdentities
            .SingleOrDefaultAsync(identity => identity.Sub == sub, ct);
        if (existingIdentity is not null)
        {
            return new UserLinkResult(
                existingIdentity.UserId == user.UserId
                    ? UserLinkStatus.Linked
                    : UserLinkStatus.CognitoSubAlreadyLinked,
                existingIdentity.UserId);
        }

        var identity = new UserIdentity
        {
            UserId = user.UserId,
            Sub = sub,
        };
        dbContext.UserIdentities.Add(identity);

        try
        {
            await dbContext.SaveChangesAsync(ct);
        }
        catch (DbUpdateException)
        {
            dbContext.Entry(identity).State = EntityState.Detached;
            existingIdentity = await dbContext.UserIdentities
                .SingleOrDefaultAsync(identity => identity.Sub == sub, ct);
            if (existingIdentity is null)
            {
                throw;
            }

            return new UserLinkResult(
                existingIdentity.UserId == user.UserId
                    ? UserLinkStatus.Linked
                    : UserLinkStatus.CognitoSubAlreadyLinked,
                existingIdentity.UserId);
        }

        return new UserLinkResult(UserLinkStatus.Linked, user.UserId);
    }

    public async Task<bool> DeactivateUserAsync(Guid userId, CancellationToken ct)
    {
        var user = await dbContext.UserProfiles
            .SingleOrDefaultAsync(profile => profile.UserId == userId, ct);
        if (user is null)
        {
            return false;
        }

        user.IsActive = false;
        user.UpdatedAtUtc = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(ct);
        return true;
    }

    private static string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();
}

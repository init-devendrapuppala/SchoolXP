using Microsoft.EntityFrameworkCore;
using SchoolService.Application.Abstractions;
using SchoolService.Application.Contracts;
using SchoolService.Infrastructure.Persistence;

namespace SchoolService.Infrastructure.Repositories;

public class SchoolMembershipRepository(SchoolDbContext context) : ISchoolMembershipRepository
{
    public async Task<IReadOnlyList<SchoolMembershipAssignment>> GetMembershipsForUserAsync(
        Guid identityUserId,
        CancellationToken ct)
    {
        return await context.SchoolMemberships
            .AsNoTracking()
            .Where(membership =>
                membership.Person.IdentityUserId == identityUserId &&
                membership.RoleType.IsActive &&
                membership.School.IsActive)
            .Select(membership => new
            {
                membership.SchoolId,
                SchoolName = membership.School.Name,
                Role = membership.RoleType.Code,
                Status = membership.MembershipStatus.Code
            })
            .Distinct()
            .OrderBy(membership => membership.SchoolName)
            .Select(membership => new SchoolMembershipAssignment(
                membership.SchoolId,
                membership.SchoolName,
                membership.Role,
                membership.Status))
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<SchoolRoleAssignment>> GetActiveRolesForUserAsync(
        Guid identityUserId,
        CancellationToken ct)
    {
        return await context.SchoolMemberships
            .AsNoTracking()
            .Where(membership =>
                membership.Person.IdentityUserId == identityUserId &&
                membership.MembershipStatus.Code == "active" &&
                membership.RoleType.IsActive &&
                membership.School.IsActive)
            .Select(membership => new
            {
                membership.SchoolId,
                SchoolName = membership.School.Name,
                Role = membership.RoleType.Code
            })
            .Distinct()
            .Select(membership => new SchoolRoleAssignment(
                membership.SchoolId,
                membership.SchoolName,
                membership.Role))
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<SchoolAssignment>> GetAllSchoolsAsync(CancellationToken ct)
    {
        return await context.Schools
            .AsNoTracking()
            .OrderBy(school => school.Name)
            .Select(school => new SchoolAssignment(
                school.SchoolId,
                school.Name,
                school.IsActive))
            .ToListAsync(ct);
    }
}
using SchoolService.Application.Contracts;

namespace SchoolService.Application.Abstractions;

public interface ISchoolMembershipRepository
{
    Task<IReadOnlyList<SchoolRoleAssignment>> GetActiveRolesForUserAsync(
        Guid identityUserId,
        CancellationToken ct);

    Task<IReadOnlyList<SchoolMembershipAssignment>> GetMembershipsForUserAsync(
        Guid identityUserId,
        CancellationToken ct);

    Task<IReadOnlyList<SchoolAssignment>> GetAllSchoolsAsync(CancellationToken ct);
}
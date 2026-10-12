using SchoolService.Application.Contracts;

namespace SchoolService.Application.Abstractions;

public interface ISchoolMembershipRepository
{
    Task<IReadOnlyList<SchoolRoleAssignment>> GetActiveRolesForUserAsync(
        Guid userId,
        CancellationToken ct);

    Task<IReadOnlyList<SchoolMembershipAssignment>> GetMembershipsForUserAsync(
        Guid userId,
        CancellationToken ct);

    Task<IReadOnlyList<SchoolAssignment>> GetAllSchoolsAsync(CancellationToken ct);
}
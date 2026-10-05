using SchoolService.Application.Contracts;

namespace SchoolService.Application.Abstractions;

public interface IStaffService
{
    Task<Guid> RegisterStaffAsync(
        StaffRegistrationData registration,
        Guid identityUserId,
        Guid schoolId,
        string createdBy,
        CancellationToken ct);
}
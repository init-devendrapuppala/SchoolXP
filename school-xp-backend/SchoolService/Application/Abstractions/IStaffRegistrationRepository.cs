using SchoolService.Application.Contracts;

namespace SchoolService.Application.Abstractions;

public interface IStaffRegistrationRepository
{
    Task<Guid> CreateDraftAsync(
        StaffRegistrationData registration,
        string encryptedAadhaar,
        Guid userId,
        Guid schoolId,
        string createdBy,
        CancellationToken ct);
}
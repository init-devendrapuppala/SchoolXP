using SchoolService.Application.Contracts;

namespace SchoolService.Application.Abstractions;

public interface IStudentRegistrationRepository
{
    Task<Guid> CreateDraftAsync(
        StudentRegistrationData registration,
        string encryptedStudentAadhaar,
        string encryptedGuardianAadhaar,
        Guid userId,
        Guid schoolId,
        string createdBy,
        CancellationToken ct);
}
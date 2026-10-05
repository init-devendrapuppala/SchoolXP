using SchoolService.Application.Abstractions;
using SchoolService.Application.Contracts;

namespace SchoolService.Application.Services;

public class StaffService(
    IStaffRegistrationRepository registrationRepository,
    IIdentityDocumentProtector identityDocumentProtector) : IStaffService
{
    public Task<Guid> RegisterStaffAsync(
        StaffRegistrationData registration,
        Guid identityUserId,
        Guid schoolId,
        string createdBy,
        CancellationToken ct)
    {
        var encryptedAadhaar = identityDocumentProtector.Protect(registration.AadhaarNumber);
        return registrationRepository.CreateDraftAsync(
            registration,
            encryptedAadhaar,
            identityUserId,
            schoolId,
            createdBy,
            ct);
    }
}
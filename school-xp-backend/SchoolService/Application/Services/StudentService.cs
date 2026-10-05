using SchoolService.Application.Abstractions;
using SchoolService.Application.Contracts;

namespace SchoolService.Application.Services
{
    public class StudentService(
        IStudentRegistrationRepository registrationRepository,
        IIdentityDocumentProtector identityDocumentProtector) : IStudentService
    {
        public async Task<Guid> RegisterStudentAsync(
            StudentRegistrationData registration,
            Guid identityUserId,
            Guid schoolId,
            string createdBy,
            CancellationToken ct)
        {
            var encryptedStudentAadhaar = identityDocumentProtector.Protect(registration.AadhaarNumber);
            var encryptedGuardianAadhaar = identityDocumentProtector.Protect(registration.GuardianAadhaarNumber);

            return await registrationRepository.CreateDraftAsync(
                registration,
                encryptedStudentAadhaar,
                encryptedGuardianAadhaar,
                identityUserId,
                schoolId,
                createdBy,
                ct);
        }
    }
}

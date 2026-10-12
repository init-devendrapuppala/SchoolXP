using SchoolService.Application.Contracts;

namespace SchoolService.Application.Abstractions
{
    public interface IStudentService
    {
        Task<Guid> RegisterStudentAsync(
            StudentRegistrationData registration,
            Guid userId,
            Guid schoolId,
            string createdBy,
            CancellationToken ct);
        
    }
}

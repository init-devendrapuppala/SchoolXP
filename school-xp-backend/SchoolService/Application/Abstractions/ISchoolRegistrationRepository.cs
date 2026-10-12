using SchoolService.Application.Contracts;

namespace SchoolService.Application.Abstractions;

public interface ISchoolRegistrationRepository
{
    Task<bool> SchoolNameExistsAsync(string name, CancellationToken ct);

    Task<Guid> RegisterSchoolAsync(
        SchoolRegistrationData registration,
        Guid userId,
        string createdBy,
        CancellationToken ct);
}

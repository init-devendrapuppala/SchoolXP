using SchoolService.API.Contracts;

namespace SchoolService.Application.Abstractions;

public interface IGuardianService
{
    Task<List<GuardianTypeDto>> GetAllGuardianTypesAsync(CancellationToken ct);
}
using SchoolService.Domain.Entities;

namespace SchoolService.Application.Abstractions;

public interface IGuardianRepository
{
    Task<List<GuardianType>> GetAllAsync(CancellationToken ct);
}
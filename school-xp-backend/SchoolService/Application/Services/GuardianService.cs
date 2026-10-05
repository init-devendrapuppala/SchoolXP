using SchoolService.API.Contracts;
using SchoolService.Application.Abstractions;

namespace SchoolService.Application.Services;

public class GuardianService(IGuardianRepository guardianRepository) : IGuardianService
{
    public async Task<List<GuardianTypeDto>> GetAllGuardianTypesAsync(CancellationToken ct)
    {
        var guardianTypes = await guardianRepository.GetAllAsync(ct);
        return guardianTypes.Select(gt => new GuardianTypeDto(gt.GuardianTypeId, gt.Code, gt.Description)).ToList();        
    }
}
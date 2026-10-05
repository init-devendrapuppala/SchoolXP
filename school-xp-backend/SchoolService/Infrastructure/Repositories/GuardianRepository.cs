using Microsoft.EntityFrameworkCore;
using SchoolService.Application.Abstractions;
using SchoolService.Domain.Entities;
using SchoolService.Infrastructure.Persistence;

namespace SchoolService.Infrastructure.Repositories;

public class GuardianRepository(SchoolDbContext context) : IGuardianRepository
{
    public async Task<List<GuardianType>> GetAllAsync(CancellationToken ct)
    {
        return await context.GuardianTypes.AsNoTracking().ToListAsync(ct);
    }
}
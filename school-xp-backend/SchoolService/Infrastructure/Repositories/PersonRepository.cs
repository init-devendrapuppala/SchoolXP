using Microsoft.EntityFrameworkCore;
using SchoolService.Application.Abstractions;
using SchoolService.Domain.Entities;
using SchoolService.Domain.Enums;
using SchoolService.Infrastructure.Persistence;

namespace SchoolService.Infrastructure.Repositories
{
    public class PersonRepository(SchoolDbContext context) : IPersonRepository
    {
        public async Task<Person> CreateAsync(Person person, CancellationToken ct)
        {
            await context.People.AddAsync(person, ct);
            await context.SaveChangesAsync(ct);
            return person;
        }

        public async Task<bool> DeletePersonAsync(Guid personId, CancellationToken ct)
        {
            var person = await context.People.Include(p => p.Memberships).SingleOrDefaultAsync(p=>p.PersonId == personId, ct);
            foreach (var ms in person.Memberships)
            {
                ms.MembershipStatusId = (int)EMembershipStatus.Inactive;
            }
            await context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<Person> GetPersonByPersonIdAsync(Guid personId, CancellationToken ct)
        {
            return await context.People.Include(p => p.Memberships).SingleOrDefaultAsync(p => p.PersonId == personId, ct);
        }

        public Task<Person> UpdatePersonAsync(Person person, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}

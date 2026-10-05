using SchoolService.Domain.Entities;

namespace SchoolService.Application.Abstractions
{
    public interface IPersonRepository
    {
        Task<Person> GetPersonByPersonIdAsync(Guid personId, CancellationToken ct);
        Task<Person> CreateAsync(Person person, CancellationToken ct);
        Task<Person> UpdatePersonAsync(Person person, CancellationToken ct);
        Task<bool> DeletePersonAsync(Guid personId, CancellationToken ct);
    }
}

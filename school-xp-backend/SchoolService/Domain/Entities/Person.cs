namespace SchoolService.Domain.Entities;

public class Person
{
    public Guid? PersonId { get; set; }
    public Guid? IdentityUserId { get; set; }
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
    public required string EmailAddress { get; set; }
    public string? Gender { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public ICollection<SchoolMembership> Memberships { get; set; } = new List<SchoolMembership>();
    public ICollection<PersonContact> Contacts { get; set; } = new List<PersonContact>();
    public ICollection<PersonAddress> Addresses { get; set; } = new List<PersonAddress>();
    public ICollection<PersonIdentityDocument> IdentityDocuments { get; set; } = new List<PersonIdentityDocument>();
    public ICollection<Guardian> Guardians { get; set; } = new List<Guardian>();
}
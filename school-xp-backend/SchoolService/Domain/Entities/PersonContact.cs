namespace SchoolService.Domain.Entities;

public class PersonContact
{
    public Guid PersonContactId { get; set; }
    public Guid PersonId { get; set; }
    public int ContactTypeId { get; set; }
    public required string Value { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsVerified { get; set; }

    public Person Person { get; set; } = null!;
    public ContactType ContactType { get; set; } = null!;
}
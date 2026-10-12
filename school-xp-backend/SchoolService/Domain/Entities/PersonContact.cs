namespace SchoolService.Domain.Entities;

public class PersonContact
{
    public Guid PersonContactId { get; set; }
    public Guid UserId { get; set; }
    public int ContactTypeId { get; set; }
    public required string Value { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsVerified { get; set; }

    public UserProfile UserProfile { get; set; } = null!;
    public ContactType ContactType { get; set; } = null!;
}
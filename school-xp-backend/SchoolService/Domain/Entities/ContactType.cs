namespace SchoolService.Domain.Entities;

public class ContactType
{
    public int ContactTypeId { get; set; }
    public required string Code { get; set; }
    public required string Description { get; set; }

    public ICollection<PersonContact> Contacts { get; set; } = new List<PersonContact>();
}
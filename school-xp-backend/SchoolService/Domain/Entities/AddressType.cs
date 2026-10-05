namespace SchoolService.Domain.Entities;

public class AddressType
{
    public int AddressTypeId { get; set; }
    public required string Code { get; set; }
    public required string Description { get; set; }

    public ICollection<PersonAddress> Addresses { get; set; } = new List<PersonAddress>();
}
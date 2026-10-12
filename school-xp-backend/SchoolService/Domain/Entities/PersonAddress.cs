namespace SchoolService.Domain.Entities;

public class PersonAddress
{
    public Guid PersonAddressId { get; set; }
    public Guid UserId { get; set; }
    public int AddressTypeId { get; set; }
    public required string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public required string City { get; set; }
    public string? StateOrProvince { get; set; }
    public required string PostalCode { get; set; }
    public required string CountryCode { get; set; }
    public bool IsPrimary { get; set; }

    public UserProfile UserProfile { get; set; } = null!;
    public AddressType AddressType { get; set; } = null!;
}
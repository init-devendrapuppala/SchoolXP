namespace SchoolService.Domain.Entities;

public class School
{
    public Guid SchoolId { get; set; }
    public required string Name { get; set; }
    public required string EmailAddress { get; set; }
    public required string PhoneNumber { get; set; }
    public required string AddressLine { get; set; }
    public required string City { get; set; }
    public required string StateOrProvince { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
    public string? Website { get; set; }
    public string? Affiliation { get; set; }
    public int? EstablishedYear { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<SchoolMembership> Memberships { get; set; } = new List<SchoolMembership>();
}
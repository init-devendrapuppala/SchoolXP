namespace SchoolService.Domain.Entities;

public class StaffEmergencyContact
{
    public Guid StaffEmergencyContactId { get; set; }
    public Guid SchoolMembershipId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Relationship { get; set; }
    public required string PhoneNumber { get; set; }
    public string? EmailAddress { get; set; }

    public SchoolMembership SchoolMembership { get; set; } = null!;
}
namespace SchoolService.Domain.Entities;

public class MemberMedicalProfile
{
    public Guid SchoolMembershipId { get; set; }
    public int? BloodGroupId { get; set; }
    public string? MedicalNotes { get; set; }

    public SchoolMembership SchoolMembership { get; set; } = null!;
    public BloodGroup? BloodGroup { get; set; }
}
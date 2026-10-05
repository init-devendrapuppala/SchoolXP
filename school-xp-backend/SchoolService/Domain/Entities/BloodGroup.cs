namespace SchoolService.Domain.Entities;

public class BloodGroup
{
    public int BloodGroupId { get; set; }
    public required string Code { get; set; }
    public required string Description { get; set; }

    public ICollection<MemberMedicalProfile> Profiles { get; set; } = new List<MemberMedicalProfile>();
}
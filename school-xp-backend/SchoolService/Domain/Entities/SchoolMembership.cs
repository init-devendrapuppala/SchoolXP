namespace SchoolService.Domain.Entities;

public class SchoolMembership
{
    public Guid SchoolMembershipId { get; set; }
    public Guid SchoolId { get; set; }
    public Guid PersonId { get; set; }
    public int RoleTypeId { get; set; }
    public int MembershipStatusId { get; set; }
    public string? MemberNumber { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
    public string? UpdatedBy { get; set; }

    public School School { get; set; } = null!;
    public Person Person { get; set; } = null!;
    public RoleType RoleType { get; set; } = null!;
    public MembershipStatus MembershipStatus { get; set; } = null!;
    public MemberMedicalProfile? MedicalProfile { get; set; }
    public ICollection<MemberDocument> Documents { get; set; } = new List<MemberDocument>();
    public ICollection<StudentPreviousSchool> PreviousSchools { get; set; } = new List<StudentPreviousSchool>();
    public ICollection<Guardian> Guardians { get; set; } = new List<Guardian>();
    public ICollection<StaffEmergencyContact> EmergencyContacts { get; set; } = new List<StaffEmergencyContact>();
}
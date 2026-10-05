namespace SchoolService.Domain.Entities;

public class MembershipStatus
{
    public int MembershipStatusId { get; set; }
    public required string Code { get; set; }
    public required string Description { get; set; }

    public ICollection<SchoolMembership> Memberships { get; set; } = new List<SchoolMembership>();
}
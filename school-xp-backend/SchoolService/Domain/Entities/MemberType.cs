namespace SchoolService.Domain.Entities;

public class RoleType
{
    public int RoleTypeId { get; set; }
    public required string Code { get; set; }
    public required string Description { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<SchoolMembership> Memberships { get; set; } = new List<SchoolMembership>();
}
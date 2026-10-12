namespace SchoolService.Domain.Entities;

public class Guardian
{
    public Guid GuardianId { get; set; }
    public Guid StudentMembershipId { get; set; }
    public Guid GuardianUserId { get; set; }
    public int GuardianTypeId { get; set; }
    public bool IsPrimary { get; set; }
    public bool CanReceiveCommunications { get; set; }
    public bool IsAuthorizedForPickup { get; set; }
    public SchoolMembership StudentMembership { get; set; } = null!;
    public UserProfile GuardianUserProfile { get; set; } = null!;
    public GuardianType RelationshipType { get; set; } = null!;
}
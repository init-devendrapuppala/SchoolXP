namespace IdentityService.Domain.Entities;

public class UserProfile
{
    public Guid UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public bool isActive { get; set; }
    public bool IsPlatformAdmin { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
    public List<UserIdentity>? UserIdentities { get; set; }
}

public class UserIdentity
{
    public required Guid UserId { get; set; }
    public required string Sub { get; set; }
}

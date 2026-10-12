namespace SchoolService.Domain.Entities;

public class UserProfile
{
    public Guid UserId { get; set; }
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Gender { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public bool IsAccount { get; set; }
    public bool IsActive { get; set; }
    public bool IsPlatformAdmin { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
    public ICollection<UserIdentity> UserIdentities { get; set; } = new List<UserIdentity>();
    public ICollection<SchoolMembership> Memberships { get; set; } = new List<SchoolMembership>();
    public ICollection<Guardian> Guardians { get; set; } = new List<Guardian>();
    public ICollection<PersonContact> Contacts { get; set; } = new List<PersonContact>();
    public ICollection<PersonAddress> Addresses { get; set; } = new List<PersonAddress>();
    public ICollection<PersonIdentityDocument> IdentityDocuments { get; set; } = new List<PersonIdentityDocument>();
}

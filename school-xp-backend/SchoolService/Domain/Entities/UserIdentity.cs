namespace SchoolService.Domain.Entities;

public class UserIdentity
{
    public Guid UserId { get; set; }
    public required string Sub { get; set; }
    public UserProfile UserProfile { get; set; } = null!;
}

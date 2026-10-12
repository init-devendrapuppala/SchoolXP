namespace SchoolService.Domain.Entities;

public class PersonIdentityDocument
{
    public Guid PersonIdentityDocumentId { get; set; }
    public Guid UserId { get; set; }
    public int IdentityDocumentTypeId { get; set; }
    public required string EncryptedIdentifier { get; set; }
    public DateTime? VerifiedAtUtc { get; set; }
    public UserProfile UserProfile { get; set; } = null!;
    public IdentityDocumentType IdentityDocumentType { get; set; } = null!;
}
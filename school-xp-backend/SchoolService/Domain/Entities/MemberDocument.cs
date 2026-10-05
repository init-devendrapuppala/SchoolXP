namespace SchoolService.Domain.Entities;

public class MemberDocument
{
    public Guid MemberDocumentId { get; set; }
    public Guid SchoolMembershipId { get; set; }
    public int DocumentTypeId { get; set; }
    public required string StorageKey { get; set; }
    public required string FileName { get; set; }
    public required string ContentType { get; set; }
    public long SizeBytes { get; set; }
    public string? ContentHash { get; set; }
    public DateTime UploadedAtUtc { get; set; }
    public required string UploadedBySubject { get; set; }
    public DateTime? ExpiresAtUtc { get; set; }
    public DateTime? VerifiedAtUtc { get; set; }

    public SchoolMembership SchoolMembership { get; set; } = null!;
    public DocumentType DocumentType { get; set; } = null!;
}
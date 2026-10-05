namespace SchoolService.Domain.Entities;

public class DocumentType
{
    public int DocumentTypeId { get; set; }
    public required string Code { get; set; }
    public required string Description { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<MemberDocument> Documents { get; set; } = new List<MemberDocument>();
}
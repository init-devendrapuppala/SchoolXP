namespace SchoolService.Domain.Entities;

public class IdentityDocumentType
{
    public int IdentityDocumentTypeId { get; set; }
    public required string Code { get; set; }
    public required string Description { get; set; }

    public ICollection<PersonIdentityDocument> IdentityDocuments { get; set; } = new List<PersonIdentityDocument>();
}
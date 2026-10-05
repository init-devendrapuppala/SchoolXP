namespace SchoolService.Domain.Entities;

public class GuardianType
{
    public int GuardianTypeId { get; set; }
    public required string Code { get; set; }
    public required string Description { get; set; }

    public ICollection<Guardian> Relationships { get; set; } = new List<Guardian>();
}
namespace SchoolService.Domain.Entities;

public class StudentPreviousSchool
{
    public Guid StudentPreviousSchoolId { get; set; }
    public Guid SchoolMembershipId { get; set; }
    public required string InstitutionName { get; set; }
    public string? City { get; set; }
    public string? CountryCode { get; set; }
    public string? LastGrade { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    public SchoolMembership SchoolMembership { get; set; } = null!;
}
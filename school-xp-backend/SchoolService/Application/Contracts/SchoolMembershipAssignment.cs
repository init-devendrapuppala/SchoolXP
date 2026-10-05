namespace SchoolService.Application.Contracts;

public sealed record SchoolMembershipAssignment(
    Guid SchoolId,
    string SchoolName,
    string Role,
    string Status);

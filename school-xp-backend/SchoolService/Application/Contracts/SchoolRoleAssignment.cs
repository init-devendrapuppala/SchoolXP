namespace SchoolService.Application.Contracts;

public sealed record SchoolRoleAssignment(Guid SchoolId, string SchoolName, string Role);
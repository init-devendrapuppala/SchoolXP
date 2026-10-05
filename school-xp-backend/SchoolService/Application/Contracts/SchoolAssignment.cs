namespace SchoolService.Application.Contracts;

public sealed record SchoolAssignment(Guid SchoolId, string SchoolName, bool IsActive);

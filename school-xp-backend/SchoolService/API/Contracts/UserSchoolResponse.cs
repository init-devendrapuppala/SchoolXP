namespace SchoolService.API.Contracts;

public sealed record UserSchoolResponse(
    Guid SchoolId,
    string Name,
    string Role,
    string Status);

namespace SchoolService.API.Contracts;

public sealed record StudentRegistrationResponse(
    Guid SchoolMembershipId,
    string Status);
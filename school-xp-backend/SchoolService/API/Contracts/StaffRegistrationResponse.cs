namespace SchoolService.API.Contracts;

public sealed record StaffRegistrationResponse(
    Guid SchoolMembershipId,
    string Status);
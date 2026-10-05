namespace SchoolService.Application.Contracts;

public sealed record StudentRegistrationData(
    string FirstName,
    string LastName,
    string Email,
    string AadhaarNumber,
    string GuardianFirstName,
    string GuardianLastName,
    string GuardianAadhaarNumber,
    int GuardianTypeId,
    string GuardianPhone,
    string GuardianEmail);
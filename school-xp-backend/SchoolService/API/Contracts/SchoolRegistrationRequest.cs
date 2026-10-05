namespace SchoolService.API.Contracts;

public sealed record SchoolRegistrationRequest(
    string SchoolName,
    string SchoolEmail,
    string SchoolPhone,
    string SchoolAddress,
    string SchoolCity,
    string SchoolState,
    string SchoolPostalCode,
    string SchoolCountry,
    string? SchoolWebsite,
    string? SchoolAffiliation,
    int? SchoolEstablishedYear,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Gender,
    DateOnly DateOfBirth);

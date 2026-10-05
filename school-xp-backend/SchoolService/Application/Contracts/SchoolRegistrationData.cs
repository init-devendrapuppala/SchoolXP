namespace SchoolService.Application.Contracts;

public sealed record SchoolRegistrationData(
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

public sealed class SchoolAlreadyExistsException(string schoolName)
    : Exception($"A school named '{schoolName}' is already registered.");

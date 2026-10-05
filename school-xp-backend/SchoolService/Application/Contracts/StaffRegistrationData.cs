namespace SchoolService.Application.Contracts;

public sealed record StaffRegistrationData(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string AadhaarNumber,
    int RoleTypeId,
    string EmergencyContactFirstName,
    string EmergencyContactLastName,
    string EmergencyContactRelationship,
    string EmergencyContactPhone,
    string? EmergencyContactEmail);
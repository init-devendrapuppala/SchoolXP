namespace SchoolService.API.Contracts;

public sealed record StaffRegistrationRequest(
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
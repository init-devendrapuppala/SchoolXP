namespace SchoolService.API.Contracts
{
    public sealed record StudentRegistrationRequest
    (
        string FirstName,
        string LastName,
        string Email,
        string AadhaarNumber,
        string GuardianFirstName,
        string GuardianLastName,
        string GuardianAadhaarNumber,
        int GuardianTypeId,
        string GuardianPhone,
        string GuardianEmail
     );
}

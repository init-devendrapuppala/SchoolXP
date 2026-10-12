namespace SchoolService.API.Contracts;

public sealed record CreateUserRequest(string UserEmail, string FirstName, string Lastname);

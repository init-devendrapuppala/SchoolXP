namespace IdentityService.Application.Contracts.User
{
    public class CreateUserRequest
    {
        public required string UserEmail { get; set; }
        public required string FirstName { get; set; }
        public required string Lastname { get; set; }
    }
}

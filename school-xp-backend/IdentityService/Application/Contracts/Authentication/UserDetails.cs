namespace IdentityService.Application.Contracts.Authentication
{
    public class UserDetails
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Sub {  get; set; }
        public bool isActive { get; set; }
        public bool IsPlatformAdmin { get; set; }
    }
}

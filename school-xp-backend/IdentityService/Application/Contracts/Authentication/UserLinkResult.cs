namespace IdentityService.Application.Contracts.Authentication;

public enum UserLinkStatus
{
    Linked,
    StudentNotFound,
    UserInactive,
    StudentAlreadyLinked,
    CognitoSubAlreadyLinked
}

public sealed record UserLinkResult(UserLinkStatus Status, Guid? UserId);
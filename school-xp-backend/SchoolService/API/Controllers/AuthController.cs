using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolService.API.Contracts;
using SchoolService.Application.Abstractions;
using SchoolService.Application.Authentication;

namespace SchoolService.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class AuthController(
    IIdentityUserService identityUserService,
    ITokenService tokenService) : ControllerBase
{
    [HttpPost("resolve-user")]
    public async Task<IActionResult> ResolveUserAsync(
        [FromBody] ResolveUserRequest request,
        CancellationToken ct)
    {
        var cognitoSub = User.FindFirstValue("sub");
        if (string.IsNullOrWhiteSpace(cognitoSub))
        {
            return Unauthorized();
        }

        string email;
        try
        {
            email = await tokenService.GetVerifiedIdTokenEmailAsync(request.IdToken, cognitoSub, ct);
        }
        catch (SecurityTokenException)
        {
            return Unauthorized();
        }

        var profile = await identityUserService.FindUserByCognitoSubAsync(cognitoSub, ct);
        if (profile is null)
        {
            var linkResult = await identityUserService.LinkCognitoSubAsync(email, cognitoSub, ct);
            if (linkResult.Status == UserLinkStatus.UserNotFound)
            {
                return NotFound(new { message = "Please use your registered email to login." });
            }

            if (linkResult.Status == UserLinkStatus.UserInactive)
            {
                return Forbid();
            }

            if (linkResult.Status == UserLinkStatus.CognitoSubAlreadyLinked)
            {
                return Conflict(new { message = "This user or Cognito account is already linked." });
            }
            if (linkResult.Status == UserLinkStatus.AmbiguousEmail)
            {
                return Conflict(new { message = "This email address matches multiple profiles; contact support." });
            }

            return Ok(new { id = linkResult.UserId, cognitoSub, email, isPlatformAdmin = false });
        }

        if (!profile.IsActive)
        {
            return Forbid();
        }

        return Ok(new
        {
            id = profile.UserId,
            cognitoSub,
            email = profile.Email,
            isPlatformAdmin = profile.IsPlatformAdmin,
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetUserAsync(CancellationToken ct)
    {
        var cognitoSub = User.FindFirstValue("sub");
        if (string.IsNullOrWhiteSpace(cognitoSub))
        {
            return Unauthorized();
        }

        var profile = await identityUserService.FindUserByCognitoSubAsync(cognitoSub, ct);
        if (profile is null)
        {
            return NotFound(new { message = "User not found." });
        }

        if (!profile.IsActive)
        {
            return Forbid();
        }

        return Ok(new
        {
            id = profile.UserId,
            cognitoSub,
            email = profile.Email,
            isPlatformAdmin = profile.IsPlatformAdmin,
        });
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUserAsync(
        [FromBody] CreateUserRequest request,
        CancellationToken ct)
    {
        if (!User.HasClaim("is_platform_admin", "true"))
        {
            return Forbid();
        }

        if (string.IsNullOrWhiteSpace(request.UserEmail) ||
            string.IsNullOrWhiteSpace(request.FirstName) ||
            string.IsNullOrWhiteSpace(request.Lastname))
        {
            return BadRequest(new { message = "User email and name are required." });
        }

        Guid userId;
        try
        {
            userId = await identityUserService.CreateOrGetUserAsync(
                request.UserEmail,
                request.FirstName,
                request.Lastname,
                ct);
        }
        catch (AmbiguousUserEmailException)
        {
            return Conflict(new { message = "This email address matches multiple profiles; contact support." });
        }

        return Ok(userId);
    }

    [HttpPost("deactivate-user")]
    public async Task<IActionResult> DeactivateUserAsync(
        [FromQuery] Guid userId,
        CancellationToken ct)
    {
        if (!User.HasClaim("is_platform_admin", "true"))
        {
            return Forbid();
        }

        if (userId == Guid.Empty || !await identityUserService.DeactivateUserAsync(userId, ct))
        {
            return NotFound(new { message = "User not found." });
        }

        return Ok();
    }
}

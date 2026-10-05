using IdentityService.Application.Abstractions;
using IdentityService.Application.Contracts.Authentication;
using IdentityService.Application.Contracts.User;
using IdentityService.API.Contracts;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityService.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuthController(IIdentityUserService idService, ITokenService tokenService) : ControllerBase
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

        string userEmail;
        try
        {
            userEmail = await tokenService.GetVerifiedIdTokenEmailAsync(
                request.IdToken,
                cognitoSub,
                ct);
        }
        catch (SecurityTokenException)
        {
            return Unauthorized();
        }

        var user = await idService.FindUserByCognitoSubAsync(cognitoSub, ct);
        if (user is null)
        {
            var linkResult = await idService.UpdateUserCognitoSubAsync(userEmail, cognitoSub, ct);

            if (linkResult.Status == UserLinkStatus.StudentNotFound)
            {
                return NotFound(new
                {
                    message = "Please use your regesitered email to login!"
                });
            }

            if (linkResult.Status == UserLinkStatus.UserInactive)
            {
                return Forbid();
            }

            if (linkResult.Status is UserLinkStatus.StudentAlreadyLinked or UserLinkStatus.CognitoSubAlreadyLinked)
            {
                return Conflict(new
                {
                    message = "This student or Cognito account is already linked."
                });
            }

            return Ok(new
            {
                id = linkResult.UserId,
                cognitoSub = cognitoSub,
                email = userEmail,
                isPlatformAdmin = false
            });
        }
        if (!user.isActive)
        {
            return Forbid();
        }
        return Ok(new
        {
            id = user.Id,
            cognitoSub = user.Sub,
            email = user.Email,
            isPlatformAdmin = user.IsPlatformAdmin
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
        var user = await idService.FindUserByCognitoSubAsync(cognitoSub, ct);
        if (user is null)
        {
            return NotFound(new
            {
                message = "User not found"
            });
        }
        if (!user.isActive)
        {
            return Forbid();
        }
        return Ok(new
        {
            id = user.Id,
            cognitoSub = user.Sub,
            email = user.Email,
            isPlatformAdmin = user.IsPlatformAdmin
        });
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUserAsync(CreateUserRequest userRequest, CancellationToken ct)
    {
        var guid = await idService.CreateUserAsync(userRequest, ct);
        return Ok(guid);
    }

    [HttpPost("deactivate-user")]
    public async Task<IActionResult> DeactivateUserAsync(Guid userId, CancellationToken ct)
    {
        await idService.DeactivateUser(userId, ct);
        return Ok();
    }
}
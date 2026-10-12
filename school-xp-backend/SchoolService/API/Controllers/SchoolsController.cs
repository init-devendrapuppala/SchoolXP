using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolService.Application.Abstractions;
using SchoolService.Application.Authentication;
using SchoolService.Application.Contracts;
using SchoolService.API.Contracts;
using System.Net.Mail;
using System.Security.Claims;

namespace SchoolService.API.Controllers;

[Authorize]
[ApiController]
[Route("api/schools")]
public class SchoolsController(
    ISchoolMembershipRepository schoolMembershipRepository,
    ISchoolRegistrationRepository schoolRegistrationRepository,
    IIdentityUserService identityUserService) : ControllerBase
{
    [HttpGet("mine")]
    public async Task<ActionResult<MySchoolsResponse>> GetMySchools(
        CancellationToken ct)
    {
        if (!Guid.TryParse(User.FindFirstValue("user_id"), out var userId) ||
            userId == Guid.Empty)
        {
            return Unauthorized();
        }

        var isPlatformAdmin = User.HasClaim("is_platform_admin", "true");
        if (isPlatformAdmin)
        {
            var allSchools = await schoolMembershipRepository.GetAllSchoolsAsync(ct);
            return Ok(new MySchoolsResponse(
                true,
                allSchools.Select(school => new UserSchoolResponse(
                    school.SchoolId,
                    school.SchoolName,
                    "platformadmin",
                    school.IsActive ? "active" : "inactive"))
                    .ToArray()));
        }

        var memberships = await schoolMembershipRepository.GetMembershipsForUserAsync(userId, ct);
        var schools = memberships
            .Select(membership => new UserSchoolResponse(
                membership.SchoolId,
                membership.SchoolName,
                membership.Role.ToLowerInvariant(),
                membership.Status.ToLowerInvariant()))
            .ToArray();

        return Ok(new MySchoolsResponse(false, schools));
    }

    [HttpPost]
    public async Task<IActionResult> RegisterSchool(
        SchoolRegistrationRequest request,
        CancellationToken ct)
    {
        if (!User.HasClaim("is_platform_admin", "true"))
        {
            return Forbid();
        }

        var email = User.FindFirstValue("email");
        if (string.IsNullOrWhiteSpace(email))
        {
            return Unauthorized();
        }

        var validationError = ValidateRegistration(request);
        if (validationError is not null)
        {
            return BadRequest(new { message = validationError });
        }

        if (await schoolRegistrationRepository.SchoolNameExistsAsync(request.SchoolName, ct))
        {
            return Conflict(new { message = "A school with this name is already registered." });
        }

        Guid userId;
        try
        {
            userId = await identityUserService.CreateOrGetUserAsync(
                request.Email.Trim(),
                request.FirstName.Trim(),
                request.LastName.Trim(),
                ct);
        }
        catch (AmbiguousUserEmailException)
        {
            return Conflict(new { message = "This email address matches multiple profiles; contact support." });
        }

        try
        {
            var schoolId = await schoolRegistrationRepository.RegisterSchoolAsync(
                new SchoolRegistrationData(
                    request.SchoolName,
                    request.SchoolEmail,
                    request.SchoolPhone,
                    request.SchoolAddress,
                    request.SchoolCity,
                    request.SchoolState,
                    request.SchoolPostalCode,
                    request.SchoolCountry,
                    request.SchoolWebsite,
                    request.SchoolAffiliation,
                    request.SchoolEstablishedYear,
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.Phone,
                    request.Gender,
                    request.DateOfBirth),
                userId,
                email,
                ct);

            return Ok(new { schoolId, adminUserId = userId });
        }
        catch (SchoolAlreadyExistsException)
        {
            return Conflict(new { message = "A school with this name is already registered." });
        }
    }

    private static string? ValidateRegistration(SchoolRegistrationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.SchoolName) ||
            string.IsNullOrWhiteSpace(request.SchoolEmail) ||
            string.IsNullOrWhiteSpace(request.SchoolPhone) ||
            string.IsNullOrWhiteSpace(request.SchoolAddress) ||
            string.IsNullOrWhiteSpace(request.SchoolCity) ||
            string.IsNullOrWhiteSpace(request.SchoolState) ||
            string.IsNullOrWhiteSpace(request.SchoolPostalCode) ||
            string.IsNullOrWhiteSpace(request.SchoolCountry) ||
            string.IsNullOrWhiteSpace(request.FirstName) ||
            string.IsNullOrWhiteSpace(request.LastName) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Phone) ||
            string.IsNullOrWhiteSpace(request.Gender))
        {
            return "Required school and school administrator details must be provided.";
        }

        if (request.SchoolName.Trim().Length > 200 ||
            request.SchoolEmail.Trim().Length > 320 ||
            request.SchoolPhone.Trim().Length > 30 ||
            request.SchoolAddress.Trim().Length > 200 ||
            request.SchoolCity.Trim().Length > 100 ||
            request.SchoolState.Trim().Length > 100 ||
            request.SchoolPostalCode.Trim().Length > 20 ||
            request.SchoolCountry.Trim().Length > 100 ||
            request.SchoolWebsite?.Trim().Length > 500 ||
            request.SchoolAffiliation?.Trim().Length > 200 ||
            request.FirstName.Trim().Length > 100 ||
            request.LastName.Trim().Length > 100 ||
            request.Email.Trim().Length > 50 ||
            request.Phone.Trim().Length > 30 ||
            request.Gender.Trim().Length > 50)
        {
            return "One or more registration fields exceed the maximum supported length.";
        }

        if (!IsEmailAddress(request.SchoolEmail) || !IsEmailAddress(request.Email))
        {
            return "The school and administrator email addresses must be valid.";
        }

        var currentYear = DateTime.UtcNow.Year;
        if (request.SchoolEstablishedYear is < 1000 || request.SchoolEstablishedYear > currentYear ||
            request.DateOfBirth == default || request.DateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return "The establishment year or administrator date of birth is invalid.";
        }

        return null;
    }

    private static bool IsEmailAddress(string value)
    {
        try
        {
            return new MailAddress(value.Trim()).Address.Equals(value.Trim(), StringComparison.OrdinalIgnoreCase);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}

public sealed record MySchoolsResponse(bool IsPlatformAdmin, IReadOnlyList<UserSchoolResponse> Schools);

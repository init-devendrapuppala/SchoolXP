using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolService.API.Contracts;
using SchoolService.Application.Abstractions;
using SchoolService.Application.Authentication;
using SchoolService.Application.Contracts;
using SchoolService.Domain.Enums;
using System.Security.Claims;

namespace SchoolService.API.Controllers;

[Authorize]
[ApiController]
[Route("api/students/registrations")]
public class StudentRegistrationController(
    IGuardianService guardianService,
    IStudentService studentService,
    IIdentityUserService identityUserService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetPageRenderInfo(CancellationToken ct)
    {
        var guardianTypes = await guardianService.GetAllGuardianTypesAsync(ct);
        return Ok(guardianTypes);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterStudent(StudentRegistrationRequest request, CancellationToken ct)
    {
        var email = User.FindFirstValue("email");
        var authorization = Request.Headers.Authorization.ToString();
        if (string.IsNullOrWhiteSpace(email) || !authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return Unauthorized();
        }

        var allowedRoles = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "platformadmin",
            "schoolgroupadmin",
            "schooladmin",
            "administrativestaff",
            "academicstaff",
            "acedemicstaff",
        };
        if (!Guid.TryParse(User.FindFirstValue("school-id"), out var schoolId) ||
            schoolId == Guid.Empty ||
            !HasAllowedRoleForSchool(User, allowedRoles, schoolId))
        {
            return Forbid();
        }

        if (string.IsNullOrWhiteSpace(request.FirstName) ||
            string.IsNullOrWhiteSpace(request.LastName) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            !IsValidAadhaar(request.AadhaarNumber) ||
            string.IsNullOrWhiteSpace(request.GuardianFirstName) ||
            string.IsNullOrWhiteSpace(request.GuardianLastName) ||
            string.IsNullOrWhiteSpace(request.GuardianEmail) ||
            !IsValidAadhaar(request.GuardianAadhaarNumber) ||
            string.IsNullOrWhiteSpace(request.GuardianPhone))
        {
            return BadRequest(new { message = "Student and guardian details are invalid or incomplete." });
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

        var membershipId = await studentService.RegisterStudentAsync(
            new StudentRegistrationData(
                request.FirstName,
                request.LastName,
                request.Email,
                request.AadhaarNumber.Trim(),
                request.GuardianFirstName,
                request.GuardianLastName,
                request.GuardianAadhaarNumber.Trim(),
                request.GuardianTypeId,
                request.GuardianPhone,
                request.GuardianEmail),
            userId,
            schoolId,
            email,
            ct);

        return StatusCode(
            StatusCodes.Status201Created,
            new StudentRegistrationResponse(
                membershipId,
                EMembershipStatus.Draft.ToString().ToLowerInvariant()));
    }

    private static bool IsValidAadhaar(string? value) =>
        value is { Length: 12 } && value.All(char.IsAsciiDigit);

    private static bool HasAllowedRoleForSchool(
        ClaimsPrincipal user,
        HashSet<string> allowedRoles,
        Guid schoolId) =>
        user.FindAll("school_role").Any(claim =>
            allowedRoles.Contains(claim.Value.Replace("_", string.Empty, StringComparison.Ordinal)) &&
            claim.Properties.TryGetValue("school_id", out var value) &&
            Guid.TryParse(value, out var roleSchoolId) &&
            roleSchoolId == schoolId);
}
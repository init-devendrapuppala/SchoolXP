using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolService.API.Contracts;
using SchoolService.Application.Abstractions;
using SchoolService.Application.Contracts;
using SchoolService.Domain.Enums;
using System.Security.Claims;

namespace SchoolService.API.Controllers;

[Authorize]
[ApiController]
[Route("api/staff/registrations")]
public class StaffRegistrationController(
    IStaffService staffService,
    IIdentityUserClient identityUserClient) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RegisterStaff(StaffRegistrationRequest request, CancellationToken ct)
    {
        var email = User.FindFirstValue("email");
        var authorization = Request.Headers.Authorization.ToString();
        if (string.IsNullOrWhiteSpace(email) ||
            !authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
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
            string.IsNullOrWhiteSpace(request.Phone) ||
            !IsValidAadhaar(request.AadhaarNumber) ||
            request.RoleTypeId is not ((int)ERoleTypes.AdministrativeStaff) and not ((int)ERoleTypes.AcedemicStaff) ||
            string.IsNullOrWhiteSpace(request.EmergencyContactFirstName) ||
            string.IsNullOrWhiteSpace(request.EmergencyContactLastName) ||
            string.IsNullOrWhiteSpace(request.EmergencyContactRelationship) ||
            string.IsNullOrWhiteSpace(request.EmergencyContactPhone))
        {
            return BadRequest(new { message = "Staff and emergency contact details are invalid or incomplete." });
        }

        var accessToken = authorization["Bearer ".Length..].Trim();
        var identityUserId = await identityUserClient.CreateOrGetUserAsync(
            request.Email.Trim(),
            request.FirstName.Trim(),
            request.LastName.Trim(),
            accessToken,
            ct);

        var membershipId = await staffService.RegisterStaffAsync(
            new StaffRegistrationData(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Phone,
                request.AadhaarNumber.Trim(),
                request.RoleTypeId,
                request.EmergencyContactFirstName,
                request.EmergencyContactLastName,
                request.EmergencyContactRelationship,
                request.EmergencyContactPhone,
                request.EmergencyContactEmail),
            identityUserId,
            schoolId,
            email,
            ct);

        return StatusCode(
            StatusCodes.Status201Created,
            new StaffRegistrationResponse(
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
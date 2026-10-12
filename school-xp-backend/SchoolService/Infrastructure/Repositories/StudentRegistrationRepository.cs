using Microsoft.EntityFrameworkCore;
using SchoolService.Application.Abstractions;
using SchoolService.Application.Contracts;
using SchoolService.Domain.Entities;
using SchoolService.Domain.Enums;
using SchoolService.Infrastructure.Persistence;

namespace SchoolService.Infrastructure.Repositories;

public class StudentRegistrationRepository(SchoolDbContext context) : IStudentRegistrationRepository
{
    public async Task<Guid> CreateDraftAsync(
        StudentRegistrationData registration,
        string encryptedStudentAadhaar,
        string encryptedGuardianAadhaar,
        Guid userId,
        Guid schoolId,
        string createdBy,
        CancellationToken ct)
    {
        var studentRoleCode = ERoleTypes.Student.ToString().ToLowerInvariant();
        var draftStatusCode = EMembershipStatus.Draft.ToString().ToLowerInvariant();
        var phoneContactTypeCode = EContactTypeCode.Phone.ToString().ToLowerInvariant();
        var aadhaarDocumentTypeCode = EIdentityDocumentTypeCode.Aadhaar.ToString().ToLowerInvariant();

        var existingUser = await context.UserProfiles
            .Include(user => user.Memberships)
                .ThenInclude(membership => membership.RoleType)
            .Include(user => user.IdentityDocuments)
            .SingleOrDefaultAsync(user => user.UserId == userId, ct);

        var studentTypeId = await context.RoleTypes
            .Where(role => role.Code == studentRoleCode)
            .Select(role => (int?)role.RoleTypeId)
            .SingleOrDefaultAsync(ct);
        if (studentTypeId is null)
        {
            throw new InvalidOperationException("The student role is not configured.");
        }

        var draftStatusId = await context.MembershipStatuses
            .Where(status => status.Code == draftStatusCode)
            .Select(status => (int?)status.MembershipStatusId)
            .SingleOrDefaultAsync(ct);
        if (draftStatusId is null)
        {
            throw new InvalidOperationException("The draft membership status is not configured.");
        }

        var guardianType = await context.GuardianTypes
            .SingleOrDefaultAsync(type => type.GuardianTypeId == registration.GuardianTypeId, ct);
        if (guardianType is null)
        {
            throw new ArgumentException("The selected guardian relationship is invalid.", nameof(registration));
        }

        var phoneType = await context.ContactTypes
            .SingleOrDefaultAsync(type => type.Code == phoneContactTypeCode, ct);
        if (phoneType is null)
        {
            throw new InvalidOperationException("The phone contact type is not configured.");
        }

        var aadhaarType = await context.IdentityDocumentTypes
            .SingleOrDefaultAsync(type => type.Code == aadhaarDocumentTypeCode, ct);
        if (aadhaarType is null)
        {
            throw new InvalidOperationException("The Aadhaar identity document type is not configured.");
        }

        var student = existingUser ?? new UserProfile
        {
            FirstName = registration.FirstName.Trim(),
            LastName = registration.LastName.Trim(),
            Email = registration.Email.Trim().ToLowerInvariant(),
            IsAccount = true,
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow,
        };

        if (existingUser is not null)
        {
            var existingMembership = existingUser.Memberships.SingleOrDefault(membership =>
                membership.SchoolId == schoolId &&
                membership.RoleType.Code == studentRoleCode);
            if (existingMembership is not null)
            {
                return existingMembership.SchoolMembershipId;
            }

            student.FirstName = registration.FirstName.Trim();
            student.LastName = registration.LastName.Trim();
            student.Email = registration.Email.Trim().ToLowerInvariant();
        }

        var guardian = new UserProfile
        {
            FirstName = registration.GuardianFirstName.Trim(),
            LastName = registration.GuardianLastName.Trim(),
            Email = registration.GuardianEmail.Trim().ToLowerInvariant(),
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow,
        };
        if (!student.IdentityDocuments.Any(document =>
                document.IdentityDocumentTypeId == aadhaarType.IdentityDocumentTypeId))
        {
            student.IdentityDocuments.Add(new PersonIdentityDocument
            {
                IdentityDocumentTypeId = aadhaarType.IdentityDocumentTypeId,
                EncryptedIdentifier = encryptedStudentAadhaar,
            });
        }
        guardian.IdentityDocuments.Add(new PersonIdentityDocument
        {
            IdentityDocumentTypeId = aadhaarType.IdentityDocumentTypeId,
            EncryptedIdentifier = encryptedGuardianAadhaar,
        });
        guardian.Contacts.Add(new PersonContact
        {
            ContactTypeId = phoneType.ContactTypeId,
            Value = registration.GuardianPhone.Trim(),
            IsPrimary = true,
        });

        var membership = new SchoolMembership
        {
            SchoolId = schoolId,
            RoleTypeId = studentTypeId.Value,
            MembershipStatusId = draftStatusId.Value,
            CreatedAtUtc = DateTime.UtcNow,
            CreatedBy = createdBy,
            UserProfile = student,
        };
        membership.Guardians.Add(new Guardian
        {
            GuardianUserProfile = guardian,
            RelationshipType = guardianType,
            IsPrimary = true,
            CanReceiveCommunications = true,
        });

        if (existingUser is null)
        {
            await context.UserProfiles.AddAsync(student, ct);
        }
        await context.UserProfiles.AddAsync(guardian, ct);
        await context.SchoolMemberships.AddAsync(membership, ct);
        await context.SaveChangesAsync(ct);

        return membership.SchoolMembershipId;
    }
}
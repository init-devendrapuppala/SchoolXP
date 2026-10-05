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
        Guid identityUserId,
        Guid schoolId,
        string createdBy,
        CancellationToken ct)
    {
        var studentRoleCode = ERoleTypes.Student.ToString().ToLowerInvariant();
        var draftStatusCode = EMembershipStatus.Draft.ToString().ToLowerInvariant();
        var phoneContactTypeCode = EContactTypeCode.Phone.ToString().ToLowerInvariant();
        var aadhaarDocumentTypeCode = EIdentityDocumentTypeCode.Aadhaar.ToString().ToLowerInvariant();

        var existingPerson = await context.People
            .Include(person => person.Memberships)
                .ThenInclude(membership => membership.RoleType)
            .Include(person => person.IdentityDocuments)
            .SingleOrDefaultAsync(person => person.IdentityUserId == identityUserId, ct);

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

        var student = existingPerson ?? new Person
        {
            IdentityUserId = identityUserId,
            FirstName = registration.FirstName.Trim(),
            LastName = registration.LastName.Trim(),
            EmailAddress = registration.Email.Trim(),
        };

        if (existingPerson is not null)
        {
            var existingMembership = existingPerson.Memberships.SingleOrDefault(membership =>
                membership.SchoolId == schoolId &&
                membership.RoleType.Code == studentRoleCode);
            if (existingMembership is not null)
            {
                return existingMembership.SchoolMembershipId;
            }

            student.FirstName = registration.FirstName.Trim();
            student.LastName = registration.LastName.Trim();
            student.EmailAddress = registration.Email.Trim();
        }

        var guardian = new Person
        {
            FirstName = registration.GuardianFirstName.Trim(),
            LastName = registration.GuardianLastName.Trim(),
            EmailAddress = registration.GuardianEmail.Trim(),
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
            Person = student,
        };
        membership.Guardians.Add(new Guardian
        {
            GuardianPerson = guardian,
            RelationshipType = guardianType,
            IsPrimary = true,
            CanReceiveCommunications = true,
        });

        if (existingPerson is null)
        {
            await context.People.AddAsync(student, ct);
        }
        await context.People.AddAsync(guardian, ct);
        await context.SchoolMemberships.AddAsync(membership, ct);
        await context.SaveChangesAsync(ct);

        return membership.SchoolMembershipId;
    }
}
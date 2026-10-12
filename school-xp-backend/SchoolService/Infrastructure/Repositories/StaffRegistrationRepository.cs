using Microsoft.EntityFrameworkCore;
using SchoolService.Application.Abstractions;
using SchoolService.Application.Contracts;
using SchoolService.Domain.Entities;
using SchoolService.Domain.Enums;
using SchoolService.Infrastructure.Persistence;

namespace SchoolService.Infrastructure.Repositories;

public class StaffRegistrationRepository(SchoolDbContext context) : IStaffRegistrationRepository
{
    public async Task<Guid> CreateDraftAsync(
        StaffRegistrationData registration,
        string encryptedAadhaar,
        Guid userId,
        Guid schoolId,
        string createdBy,
        CancellationToken ct)
    {
        var allowedRoleIds = new[]
        {
            (int)ERoleTypes.AdministrativeStaff,
            (int)ERoleTypes.AcedemicStaff,
        };
        if (!allowedRoleIds.Contains(registration.RoleTypeId))
        {
            throw new ArgumentException("Only academic or administrative staff can be registered.", nameof(registration));
        }

        var roleCode = ((ERoleTypes)registration.RoleTypeId).ToString().ToLowerInvariant();
        var draftStatusCode = EMembershipStatus.Draft.ToString().ToLowerInvariant();
        var phoneContactCode = EContactTypeCode.Phone.ToString().ToLowerInvariant();
        var aadhaarDocumentCode = EIdentityDocumentTypeCode.Aadhaar.ToString().ToLowerInvariant();

        var existingUser = await context.UserProfiles
            .Include(user => user.Memberships)
                .ThenInclude(membership => membership.RoleType)
            .Include(user => user.IdentityDocuments)
            .SingleOrDefaultAsync(user => user.UserId == userId, ct);

        var roleTypeId = await context.RoleTypes
            .Where(role => role.Code == roleCode)
            .Select(role => (int?)role.RoleTypeId)
            .SingleOrDefaultAsync(ct);
        if (roleTypeId is null)
        {
            throw new InvalidOperationException("The selected staff role is not configured.");
        }

        var draftStatusId = await context.MembershipStatuses
            .Where(status => status.Code == draftStatusCode)
            .Select(status => (int?)status.MembershipStatusId)
            .SingleOrDefaultAsync(ct);
        if (draftStatusId is null)
        {
            throw new InvalidOperationException("The draft membership status is not configured.");
        }

        var phoneTypeId = await context.ContactTypes
            .Where(type => type.Code == phoneContactCode)
            .Select(type => (int?)type.ContactTypeId)
            .SingleOrDefaultAsync(ct);
        if (phoneTypeId is null)
        {
            throw new InvalidOperationException("The phone contact type is not configured.");
        }

        var aadhaarTypeId = await context.IdentityDocumentTypes
            .Where(type => type.Code == aadhaarDocumentCode)
            .Select(type => (int?)type.IdentityDocumentTypeId)
            .SingleOrDefaultAsync(ct);
        if (aadhaarTypeId is null)
        {
            throw new InvalidOperationException("The Aadhaar identity document type is not configured.");
        }

        var staff = existingUser ?? new UserProfile
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
                membership.RoleType.Code == roleCode);
            if (existingMembership is not null)
            {
                return existingMembership.SchoolMembershipId;
            }

            staff.FirstName = registration.FirstName.Trim();
            staff.LastName = registration.LastName.Trim();
            staff.Email = registration.Email.Trim().ToLowerInvariant();
        }

        if (!staff.IdentityDocuments.Any(document => document.IdentityDocumentTypeId == aadhaarTypeId.Value))
        {
            staff.IdentityDocuments.Add(new PersonIdentityDocument
            {
                IdentityDocumentTypeId = aadhaarTypeId.Value,
                EncryptedIdentifier = encryptedAadhaar,
            });
        }

        staff.Contacts.Add(new PersonContact
        {
            ContactTypeId = phoneTypeId.Value,
            Value = registration.Phone.Trim(),
            IsPrimary = true,
        });

        var membership = new SchoolMembership
        {
            SchoolId = schoolId,
            RoleTypeId = roleTypeId.Value,
            MembershipStatusId = draftStatusId.Value,
            CreatedAtUtc = DateTime.UtcNow,
            CreatedBy = createdBy,
            UserProfile = staff,
        };
        membership.EmergencyContacts.Add(new StaffEmergencyContact
        {
            FirstName = registration.EmergencyContactFirstName.Trim(),
            LastName = registration.EmergencyContactLastName.Trim(),
            Relationship = registration.EmergencyContactRelationship.Trim(),
            PhoneNumber = registration.EmergencyContactPhone.Trim(),
            EmailAddress = string.IsNullOrWhiteSpace(registration.EmergencyContactEmail)
                ? null
                : registration.EmergencyContactEmail.Trim(),
        });

        if (existingUser is null)
        {
            await context.UserProfiles.AddAsync(staff, ct);
        }
        await context.SchoolMemberships.AddAsync(membership, ct);
        await context.SaveChangesAsync(ct);

        return membership.SchoolMembershipId;
    }
}
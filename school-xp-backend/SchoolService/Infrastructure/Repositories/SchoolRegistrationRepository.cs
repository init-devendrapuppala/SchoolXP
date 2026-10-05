using Microsoft.EntityFrameworkCore;
using SchoolService.Application.Abstractions;
using SchoolService.Application.Contracts;
using SchoolService.Domain.Entities;
using SchoolService.Domain.Enums;
using SchoolService.Infrastructure.Persistence;

namespace SchoolService.Infrastructure.Repositories;

public class SchoolRegistrationRepository(SchoolDbContext context) : ISchoolRegistrationRepository
{
    public Task<bool> SchoolNameExistsAsync(string name, CancellationToken ct)
    {
        var normalizedName = name.Trim().ToUpperInvariant();
        return context.Schools.AnyAsync(
            school => school.Name.ToUpper() == normalizedName,
            ct);
    }

    public async Task<Guid> RegisterSchoolAsync(
        SchoolRegistrationData registration,
        Guid identityUserId,
        string createdBy,
        CancellationToken ct)
    {
        if (await SchoolNameExistsAsync(registration.SchoolName, ct))
        {
            throw new SchoolAlreadyExistsException(registration.SchoolName);
        }

        await using var transaction = await context.Database.BeginTransactionAsync(ct);

        var schoolAdminRole = await context.RoleTypes
            .SingleOrDefaultAsync(
                role => role.Code == ERoleTypes.SchoolAdmin.ToString().ToLowerInvariant(),
                ct);
        if (schoolAdminRole is null)
        {
            schoolAdminRole = new RoleType
            {
                Code = ERoleTypes.SchoolAdmin.ToString().ToLowerInvariant(),
                Description = "School Admin",
                IsActive = true,
            };
            await context.RoleTypes.AddAsync(schoolAdminRole, ct);
            await context.SaveChangesAsync(ct);
        }
        else if (!schoolAdminRole.IsActive)
        {
            throw new InvalidOperationException("The school admin role is inactive.");
        }

        var activeStatusId = await context.MembershipStatuses
            .Where(status => status.Code == EMembershipStatus.Active.ToString().ToLowerInvariant())
            .Select(status => (int?)status.MembershipStatusId)
            .SingleOrDefaultAsync(ct);
        if (activeStatusId is null)
        {
            throw new InvalidOperationException("The active membership status is not configured.");
        }

        var phoneType = await context.ContactTypes
            .SingleOrDefaultAsync(
                type => type.Code == EContactTypeCode.Phone.ToString().ToLowerInvariant(),
                ct);
        if (phoneType is null)
        {
            phoneType = new ContactType
            {
                Code = EContactTypeCode.Phone.ToString().ToLowerInvariant(),
                Description = "Phone",
            };
            await context.ContactTypes.AddAsync(phoneType, ct);
            await context.SaveChangesAsync(ct);
        }

        var school = new School
        {
            SchoolId = Guid.NewGuid(),
            Name = registration.SchoolName.Trim(),
            EmailAddress = registration.SchoolEmail.Trim(),
            PhoneNumber = registration.SchoolPhone.Trim(),
            AddressLine = registration.SchoolAddress.Trim(),
            City = registration.SchoolCity.Trim(),
            StateOrProvince = registration.SchoolState.Trim(),
            PostalCode = registration.SchoolPostalCode.Trim(),
            Country = registration.SchoolCountry.Trim(),
            Website = NullIfWhiteSpace(registration.SchoolWebsite),
            Affiliation = NullIfWhiteSpace(registration.SchoolAffiliation),
            EstablishedYear = registration.SchoolEstablishedYear,
            IsActive = true,
        };
        await context.Schools.AddAsync(school, ct);

        var person = await context.People
            .Include(existingPerson => existingPerson.Contacts)
            .SingleOrDefaultAsync(existingPerson => existingPerson.IdentityUserId == identityUserId, ct);
        if (person is null)
        {
            person = new Person
            {
                IdentityUserId = identityUserId,
                FirstName = registration.FirstName.Trim(),
                LastName = registration.LastName.Trim(),
                EmailAddress = registration.Email.Trim(),
                Gender = registration.Gender.Trim(),
                DateOfBirth = registration.DateOfBirth,
            };
            await context.People.AddAsync(person, ct);
        }
        else
        {
            person.FirstName = registration.FirstName.Trim();
            person.LastName = registration.LastName.Trim();
            person.EmailAddress = registration.Email.Trim();
            person.Gender = registration.Gender.Trim();
            person.DateOfBirth = registration.DateOfBirth;
        }

        var phoneContact = person.Contacts.FirstOrDefault(        contact => contact.ContactTypeId == phoneType.ContactTypeId);
        if (phoneContact is null)
        {
            person.Contacts.Add(new PersonContact
            {
                ContactTypeId = phoneType.ContactTypeId,
                Value = registration.Phone.Trim(),
                IsPrimary = true,
            });
        }
        else
        {
            phoneContact.Value = registration.Phone.Trim();
            phoneContact.IsPrimary = true;
        }

        await context.SchoolMemberships.AddAsync(new SchoolMembership
        {
            SchoolId = school.SchoolId,
            Person = person,
            RoleTypeId = schoolAdminRole.RoleTypeId,
            MembershipStatusId = activeStatusId.Value,
            CreatedAtUtc = DateTime.UtcNow,
            CreatedBy = createdBy,
        }, ct);

        await context.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);
        return school.SchoolId;
    }

    private static string? NullIfWhiteSpace(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}

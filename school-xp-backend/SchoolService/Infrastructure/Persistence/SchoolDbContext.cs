using Microsoft.EntityFrameworkCore;
using SchoolService.Domain.Entities;

namespace SchoolService.Infrastructure.Persistence;

public class SchoolDbContext(DbContextOptions<SchoolDbContext> options) : DbContext(options)
{
	public DbSet<School> Schools => Set<School>();
	public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
	public DbSet<UserIdentity> UserIdentities => Set<UserIdentity>();
	public DbSet<RoleType> RoleTypes => Set<RoleType>();
	public DbSet<MembershipStatus> MembershipStatuses => Set<MembershipStatus>();
	public DbSet<SchoolMembership> SchoolMemberships => Set<SchoolMembership>();
	public DbSet<ContactType> ContactTypes => Set<ContactType>();
	public DbSet<PersonContact> PersonContacts => Set<PersonContact>();
	public DbSet<AddressType> AddressTypes => Set<AddressType>();
	public DbSet<PersonAddress> PersonAddresses => Set<PersonAddress>();
	public DbSet<BloodGroup> BloodGroups => Set<BloodGroup>();
	public DbSet<MemberMedicalProfile> MemberMedicalProfiles => Set<MemberMedicalProfile>();
	public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();
	public DbSet<MemberDocument> MemberDocuments => Set<MemberDocument>();
	public DbSet<StudentPreviousSchool> StudentPreviousSchools => Set<StudentPreviousSchool>();
	public DbSet<GuardianType> GuardianTypes => Set<GuardianType>();
	public DbSet<Guardian> Guardians => Set<Guardian>();
	public DbSet<IdentityDocumentType> IdentityDocumentTypes => Set<IdentityDocumentType>();
	public DbSet<PersonIdentityDocument> PersonIdentityDocuments => Set<PersonIdentityDocument>();
	public DbSet<StaffEmergencyContact> StaffEmergencyContacts => Set<StaffEmergencyContact>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.HasDefaultSchema("sd");

		modelBuilder.Entity<School>(entity =>
		{
			entity.ToTable("schools", "sm");
			entity.HasKey(school => school.SchoolId);
			entity.Property(school => school.Name).HasMaxLength(200).IsRequired();
			entity.Property(school => school.EmailAddress).HasMaxLength(320).IsRequired();
			entity.Property(school => school.PhoneNumber).HasMaxLength(30).IsRequired();
			entity.Property(school => school.AddressLine).HasMaxLength(200).IsRequired();
			entity.Property(school => school.City).HasMaxLength(100).IsRequired();
			entity.Property(school => school.StateOrProvince).HasMaxLength(100).IsRequired();
			entity.Property(school => school.PostalCode).HasMaxLength(20).IsRequired();
			entity.Property(school => school.Country).HasMaxLength(100).IsRequired();
			entity.Property(school => school.Website).HasMaxLength(500);
			entity.Property(school => school.Affiliation).HasMaxLength(200);
		});

		modelBuilder.Entity<UserProfile>(entity =>
		{
			entity.ToTable("user_profiles", "per");
			entity.HasKey(user => user.UserId);
			entity.Property(user => user.UserId).HasDefaultValueSql("gen_random_uuid()");
			entity.Property(user => user.FirstName).HasMaxLength(100).IsRequired();
			entity.Property(user => user.MiddleName).HasMaxLength(100);
			entity.Property(user => user.LastName).HasMaxLength(100).IsRequired();
			entity.Property(user => user.Email).HasMaxLength(320).IsRequired();
			entity.Property(user => user.IsAccount).HasDefaultValue(false).IsRequired();
			entity.HasIndex(user => user.Email)
				.IsUnique()
				.HasFilter("\"is_account\" = TRUE");
			entity.Property(user => user.Gender).HasMaxLength(50);
			entity.Property(user => user.IsActive).IsRequired();
			entity.Property(user => user.IsPlatformAdmin).HasDefaultValue(false).IsRequired();
			entity.Property(user => user.CreatedAtUtc).IsRequired();
		});

		modelBuilder.Entity<UserIdentity>(entity =>
		{
			entity.ToTable("user_identities", "per");
			entity.HasKey(identity => new { identity.UserId, identity.Sub });
			entity.Property(identity => identity.Sub).HasMaxLength(200).IsRequired();
			entity.HasIndex(identity => identity.Sub).IsUnique();
			entity.HasOne(identity => identity.UserProfile)
				.WithMany(user => user.UserIdentities)
				.HasForeignKey(identity => identity.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<RoleType>(entity =>
		{
			entity.ToTable("member_types", "sm");
			entity.HasKey(type => type.RoleTypeId);
			entity.Property(type => type.Code).HasMaxLength(32).IsRequired();
			entity.Property(type => type.Description).HasMaxLength(100).IsRequired();
			entity.HasIndex(type => type.Code).IsUnique();
		});

		modelBuilder.Entity<MembershipStatus>(entity =>
		{
			entity.ToTable("membership_statuses", "sm");
			entity.HasKey(status => status.MembershipStatusId);
			entity.Property(status => status.Code).HasMaxLength(32).IsRequired();
			entity.Property(status => status.Description).HasMaxLength(100).IsRequired();
			entity.HasIndex(status => status.Code).IsUnique();
			entity.HasData(
				new MembershipStatus { MembershipStatusId = 1, Code = "draft", Description = "Draft" },
				new MembershipStatus { MembershipStatusId = 2, Code = "pending", Description = "Pending" },
				new MembershipStatus { MembershipStatusId = 3, Code = "active", Description = "Active" },
				new MembershipStatus { MembershipStatusId = 4, Code = "inactive", Description = "Inactive" });
		});

		modelBuilder.Entity<SchoolMembership>(entity =>
		{
			entity.HasKey(membership => membership.SchoolMembershipId);
			entity.Property(membership => membership.MemberNumber).HasMaxLength(50);
			entity.Property(membership => membership.CreatedBy).HasMaxLength(200).IsRequired();
			entity.Property(membership => membership.UpdatedBy).HasMaxLength(200);
			entity.HasIndex(membership => new { membership.SchoolId, membership.MemberNumber })
				.IsUnique()
				.HasFilter("\"MemberNumber\" IS NOT NULL");
			entity.HasOne(membership => membership.School)
				.WithMany(school => school.Memberships)
				.HasForeignKey(membership => membership.SchoolId)
				.OnDelete(DeleteBehavior.Restrict);
			entity.HasOne(membership => membership.UserProfile)
				.WithMany(user => user.Memberships)
				.HasForeignKey(membership => membership.UserId)
				.OnDelete(DeleteBehavior.Restrict);
			entity.HasOne(membership => membership.RoleType)
				.WithMany(type => type.Memberships)
				.HasForeignKey(membership => membership.RoleTypeId)
				.OnDelete(DeleteBehavior.Restrict);
			entity.HasOne(membership => membership.MembershipStatus)
				.WithMany(status => status.Memberships)
				.HasForeignKey(membership => membership.MembershipStatusId)
				.OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<StaffEmergencyContact>(entity =>
		{
			entity.ToTable("staff_emergency_contacts", "sd");
			entity.HasKey(contact => contact.StaffEmergencyContactId);
			entity.Property(contact => contact.FirstName).HasMaxLength(100).IsRequired();
			entity.Property(contact => contact.LastName).HasMaxLength(100).IsRequired();
			entity.Property(contact => contact.Relationship).HasMaxLength(100).IsRequired();
			entity.Property(contact => contact.PhoneNumber).HasMaxLength(30).IsRequired();
			entity.Property(contact => contact.EmailAddress).HasMaxLength(320);
			entity.HasOne(contact => contact.SchoolMembership)
				.WithMany(membership => membership.EmergencyContacts)
				.HasForeignKey(contact => contact.SchoolMembershipId)
				.HasConstraintName("fk_staff_emergency_contacts_membership")
				.OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<ContactType>(entity =>
		{
			entity.ToTable("contact_types", "sm");
			entity.HasKey(type => type.ContactTypeId);
			entity.Property(type => type.Code).HasMaxLength(32).IsRequired();
			entity.Property(type => type.Description).HasMaxLength(100).IsRequired();
			entity.HasIndex(type => type.Code).IsUnique();
		});

		modelBuilder.Entity<PersonContact>(entity =>
		{
			entity.ToTable("person_contacts", "per");
			entity.HasKey(contact => contact.PersonContactId);
			entity.Property(contact => contact.Value).HasMaxLength(320).IsRequired();
			entity.HasOne(contact => contact.UserProfile)
				.WithMany(user => user.Contacts)
				.HasForeignKey(contact => contact.UserId)
				.OnDelete(DeleteBehavior.Restrict);
			entity.HasOne(contact => contact.ContactType)
				.WithMany(type => type.Contacts)
				.HasForeignKey(contact => contact.ContactTypeId)
				.OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<AddressType>(entity =>
		{
			entity.ToTable("address_types", "sm");
			entity.HasKey(type => type.AddressTypeId);
			entity.Property(type => type.Code).HasMaxLength(32).IsRequired();
			entity.Property(type => type.Description).HasMaxLength(100).IsRequired();
			entity.HasIndex(type => type.Code).IsUnique();
		});

		modelBuilder.Entity<PersonAddress>(entity =>
		{
			entity.ToTable("person_addresses", "per");
			entity.HasKey(address => address.PersonAddressId);
			entity.Property(address => address.AddressLine1).HasMaxLength(200).IsRequired();
			entity.Property(address => address.AddressLine2).HasMaxLength(200);
			entity.Property(address => address.City).HasMaxLength(100).IsRequired();
			entity.Property(address => address.StateOrProvince).HasMaxLength(100);
			entity.Property(address => address.PostalCode).HasMaxLength(20).IsRequired();
			entity.Property(address => address.CountryCode).HasMaxLength(2).IsRequired();
			entity.HasOne(address => address.UserProfile)
				.WithMany(user => user.Addresses)
				.HasForeignKey(address => address.UserId)
				.OnDelete(DeleteBehavior.Restrict);
			entity.HasOne(address => address.AddressType)
				.WithMany(type => type.Addresses)
				.HasForeignKey(address => address.AddressTypeId)
				.OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<BloodGroup>(entity =>
		{
			entity.ToTable("blood_groups", "sm");
			entity.HasKey(group => group.BloodGroupId);
			entity.Property(group => group.Code).HasMaxLength(20).IsRequired();
			entity.Property(group => group.Description).HasMaxLength(50).IsRequired();
			entity.HasIndex(group => group.Code).IsUnique();
		});

		modelBuilder.Entity<MemberMedicalProfile>(entity =>
		{
			entity.HasKey(profile => profile.SchoolMembershipId);
			entity.Property(profile => profile.MedicalNotes).HasMaxLength(2000);
			entity.HasOne(profile => profile.SchoolMembership)
				.WithOne(membership => membership.MedicalProfile)
				.HasForeignKey<MemberMedicalProfile>(profile => profile.SchoolMembershipId)
				.OnDelete(DeleteBehavior.Restrict);
			entity.HasOne(profile => profile.BloodGroup)
				.WithMany(group => group.Profiles)
				.HasForeignKey(profile => profile.BloodGroupId)
				.OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<DocumentType>(entity =>
		{
			entity.ToTable("document_types", "sm");
			entity.HasKey(type => type.DocumentTypeId);
			entity.Property(type => type.Code).HasMaxLength(32).IsRequired();
			entity.Property(type => type.Description).HasMaxLength(100).IsRequired();
			entity.HasIndex(type => type.Code).IsUnique();
		});

		modelBuilder.Entity<MemberDocument>(entity =>
		{
			entity.HasKey(document => document.MemberDocumentId);
			entity.Property(document => document.StorageKey).HasMaxLength(1024).IsRequired();
			entity.Property(document => document.FileName).HasMaxLength(255).IsRequired();
			entity.Property(document => document.ContentType).HasMaxLength(200).IsRequired();
			entity.Property(document => document.ContentHash).HasMaxLength(128);
			entity.Property(document => document.UploadedBySubject).HasMaxLength(200).IsRequired();
			entity.HasOne(document => document.SchoolMembership)
				.WithMany(membership => membership.Documents)
				.HasForeignKey(document => document.SchoolMembershipId)
				.OnDelete(DeleteBehavior.Restrict);
			entity.HasOne(document => document.DocumentType)
				.WithMany(type => type.Documents)
				.HasForeignKey(document => document.DocumentTypeId)
				.OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<StudentPreviousSchool>(entity =>
		{
			entity.HasKey(previousSchool => previousSchool.StudentPreviousSchoolId);
			entity.Property(previousSchool => previousSchool.InstitutionName).HasMaxLength(200).IsRequired();
			entity.Property(previousSchool => previousSchool.City).HasMaxLength(100);
			entity.Property(previousSchool => previousSchool.CountryCode).HasMaxLength(2);
			entity.Property(previousSchool => previousSchool.LastGrade).HasMaxLength(50);
			entity.HasOne(previousSchool => previousSchool.SchoolMembership)
				.WithMany(membership => membership.PreviousSchools)
				.HasForeignKey(previousSchool => previousSchool.SchoolMembershipId)
				.OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<GuardianType>(entity =>
		{
			entity.ToTable("guardian_types", "sm");
			entity.HasKey(type => type.GuardianTypeId);
			entity.Property(type => type.Code).HasMaxLength(32).IsRequired();
			entity.Property(type => type.Description).HasMaxLength(100).IsRequired();
			entity.HasIndex(type => type.Code).IsUnique();
		});

		modelBuilder.Entity<Guardian>(entity =>
		{
			entity.HasKey(relationship => relationship.GuardianId);
			entity.HasIndex(relationship => new
			{
				relationship.StudentMembershipId,
				relationship.GuardianUserId
			}).IsUnique();
			entity.HasOne(relationship => relationship.StudentMembership)
				.WithMany(membership => membership.Guardians)
				.HasForeignKey(relationship => relationship.StudentMembershipId)
				.OnDelete(DeleteBehavior.Restrict);
			entity.HasOne(relationship => relationship.GuardianUserProfile)
				.WithMany(user => user.Guardians)
				.HasForeignKey(relationship => relationship.GuardianUserId)
				.OnDelete(DeleteBehavior.Restrict);
			entity.HasOne(relationship => relationship.RelationshipType)
				.WithMany(type => type.Relationships)
				.HasForeignKey(relationship => relationship.GuardianTypeId)
				.OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<IdentityDocumentType>(entity =>
		{
			entity.ToTable("identity_document_types", "sm");
			entity.HasKey(type => type.IdentityDocumentTypeId);
			entity.Property(type => type.Code).HasMaxLength(32).IsRequired();
			entity.Property(type => type.Description).HasMaxLength(100).IsRequired();
			entity.HasIndex(type => type.Code).IsUnique();
		});

		modelBuilder.Entity<PersonIdentityDocument>(entity =>
		{
			entity.ToTable("person_identity_documents", "per");
			entity.HasKey(document => document.PersonIdentityDocumentId);
			entity.Property(document => document.EncryptedIdentifier).HasMaxLength(2048).IsRequired();
			entity.HasOne(document => document.UserProfile)
				.WithMany(user => user.IdentityDocuments)
				.HasForeignKey(document => document.UserId)
				.OnDelete(DeleteBehavior.Restrict);
			entity.HasOne(document => document.IdentityDocumentType)
				.WithMany(type => type.IdentityDocuments)
				.HasForeignKey(document => document.IdentityDocumentTypeId)
				.OnDelete(DeleteBehavior.Restrict);
		});
	}
}
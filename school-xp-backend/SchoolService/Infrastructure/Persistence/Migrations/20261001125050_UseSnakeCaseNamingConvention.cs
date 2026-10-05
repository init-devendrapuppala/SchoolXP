using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UseSnakeCaseNamingConvention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guardians_GuardianTypes_GuardianRel~",
                schema: "sd",
                table: "Guardians");

            migrationBuilder.DropForeignKey(
                name: "FK_Guardians_People_GuardianPersonId",
                schema: "sd",
                table: "Guardians");

            migrationBuilder.DropForeignKey(
                name: "FK_Guardians_SchoolMemberships_StudentMembershipId",
                schema: "sd",
                table: "Guardians");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberDocuments_DocumentTypes_DocumentTypeId",
                schema: "sd",
                table: "MemberDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberDocuments_SchoolMemberships_SchoolMembershipId",
                schema: "sd",
                table: "MemberDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberMedicalProfiles_BloodGroups_BloodGroupId",
                schema: "sd",
                table: "MemberMedicalProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberMedicalProfiles_SchoolMemberships_SchoolMembershipId",
                schema: "sd",
                table: "MemberMedicalProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonAddresses_AddressTypes_AddressTypeId",
                schema: "per",
                table: "PersonAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonAddresses_People_PersonId",
                schema: "per",
                table: "PersonAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonContacts_ContactTypes_ContactTypeId",
                schema: "per",
                table: "PersonContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonContacts_People_PersonId",
                schema: "per",
                table: "PersonContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonIdentityDocuments_IdentityDocumentTypes_IdentityDocum~",
                schema: "per",
                table: "PersonIdentityDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonIdentityDocuments_People_PersonId",
                schema: "per",
                table: "PersonIdentityDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolMemberships_RoleTypes_RoleTypeId",
                schema: "sd",
                table: "SchoolMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolMemberships_People_PersonId",
                schema: "sd",
                table: "SchoolMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolMemberships_Schools_SchoolId",
                schema: "sd",
                table: "SchoolMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPreviousSchools_SchoolMemberships_SchoolMembershipId",
                schema: "sd",
                table: "StudentPreviousSchools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schools",
                schema: "sm",
                table: "Schools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                schema: "per",
                table: "People");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentPreviousSchools",
                schema: "sd",
                table: "StudentPreviousSchools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolMemberships",
                schema: "sd",
                table: "SchoolMemberships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonIdentityDocuments",
                schema: "per",
                table: "PersonIdentityDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonContacts",
                schema: "per",
                table: "PersonContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonAddresses",
                schema: "per",
                table: "PersonAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleTypes",
                schema: "sm",
                table: "RoleTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberMedicalProfiles",
                schema: "sd",
                table: "MemberMedicalProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberDocuments",
                schema: "sd",
                table: "MemberDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityDocumentTypes",
                schema: "sm",
                table: "IdentityDocumentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GuardianTypes",
                schema: "sm",
                table: "GuardianTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guardians",
                schema: "sd",
                table: "Guardians");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentTypes",
                schema: "sm",
                table: "DocumentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactTypes",
                schema: "sm",
                table: "ContactTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BloodGroups",
                schema: "sm",
                table: "BloodGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressTypes",
                schema: "sm",
                table: "AddressTypes");

            migrationBuilder.RenameTable(
                name: "Schools",
                schema: "sm",
                newName: "schools",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "People",
                schema: "per",
                newName: "people",
                newSchema: "per");

            migrationBuilder.RenameTable(
                name: "StudentPreviousSchools",
                schema: "sd",
                newName: "student_previous_schools",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "SchoolMemberships",
                schema: "sd",
                newName: "school_memberships",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "PersonIdentityDocuments",
                schema: "per",
                newName: "person_identity_documents",
                newSchema: "per");

            migrationBuilder.RenameTable(
                name: "PersonContacts",
                schema: "per",
                newName: "person_contacts",
                newSchema: "per");

            migrationBuilder.RenameTable(
                name: "PersonAddresses",
                schema: "per",
                newName: "person_addresses",
                newSchema: "per");

            migrationBuilder.RenameTable(
                name: "RoleTypes",
                schema: "sm",
                newName: "member_types",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "MemberMedicalProfiles",
                schema: "sd",
                newName: "member_medical_profiles",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "MemberDocuments",
                schema: "sd",
                newName: "member_documents",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "IdentityDocumentTypes",
                schema: "sm",
                newName: "identity_document_types",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "GuardianTypes",
                schema: "sm",
                newName: "guardian_relationship_types",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "Guardians",
                schema: "sd",
                newName: "guardian_relationships",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "DocumentTypes",
                schema: "sm",
                newName: "document_types",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "ContactTypes",
                schema: "sm",
                newName: "contact_types",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "BloodGroups",
                schema: "sm",
                newName: "blood_groups",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "AddressTypes",
                schema: "sm",
                newName: "address_types",
                newSchema: "sm");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "sm",
                table: "schools",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "sm",
                table: "schools",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "SchoolId",
                schema: "sm",
                table: "schools",
                newName: "school_id");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                schema: "per",
                table: "people",
                newName: "middle_name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "per",
                table: "people",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                schema: "per",
                table: "people",
                newName: "identity_user_id");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "per",
                table: "people",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                schema: "per",
                table: "people",
                newName: "person_id");

            migrationBuilder.RenameIndex(
                name: "IX_People_IdentityUserId",
                schema: "per",
                table: "people",
                newName: "ix_people_identity_user_id");

            migrationBuilder.RenameColumn(
                name: "City",
                schema: "sd",
                table: "student_previous_schools",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                schema: "sd",
                table: "student_previous_schools",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "SchoolMembershipId",
                schema: "sd",
                table: "student_previous_schools",
                newName: "school_membership_id");

            migrationBuilder.RenameColumn(
                name: "LastGrade",
                schema: "sd",
                table: "student_previous_schools",
                newName: "last_grade");

            migrationBuilder.RenameColumn(
                name: "InstitutionName",
                schema: "sd",
                table: "student_previous_schools",
                newName: "institution_name");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                schema: "sd",
                table: "student_previous_schools",
                newName: "end_date");

            migrationBuilder.RenameColumn(
                name: "CountryCode",
                schema: "sd",
                table: "student_previous_schools",
                newName: "country_code");

            migrationBuilder.RenameColumn(
                name: "StudentPreviousSchoolId",
                schema: "sd",
                table: "student_previous_schools",
                newName: "student_previous_school_id");

            migrationBuilder.RenameIndex(
                name: "IX_StudentPreviousSchools_SchoolMembershipId",
                schema: "sd",
                table: "student_previous_schools",
                newName: "ix_student_previous_schools_school_membership_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "sd",
                table: "school_memberships",
                newName: "updated_by_subject");

            migrationBuilder.RenameColumn(
                name: "UpdatedAtUtc",
                schema: "sd",
                table: "school_memberships",
                newName: "updated_at_utc");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                schema: "sd",
                table: "school_memberships",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "SchoolId",
                schema: "sd",
                table: "school_memberships",
                newName: "school_id");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                schema: "sd",
                table: "school_memberships",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "RoleTypeId",
                schema: "sd",
                table: "school_memberships",
                newName: "member_type_id");

            migrationBuilder.RenameColumn(
                name: "MemberNumber",
                schema: "sd",
                table: "school_memberships",
                newName: "member_number");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "sd",
                table: "school_memberships",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                schema: "sd",
                table: "school_memberships",
                newName: "end_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "sd",
                table: "school_memberships",
                newName: "created_by_subject");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                schema: "sd",
                table: "school_memberships",
                newName: "created_at_utc");

            migrationBuilder.RenameColumn(
                name: "SchoolMembershipId",
                schema: "sd",
                table: "school_memberships",
                newName: "school_membership_id");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolMemberships_SchoolId_MemberNumber",
                schema: "sd",
                table: "school_memberships",
                newName: "ix_school_memberships_school_id_member_number");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolMemberships_PersonId",
                schema: "sd",
                table: "school_memberships",
                newName: "ix_school_memberships_person_id");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolMemberships_RoleTypeId",
                schema: "sd",
                table: "school_memberships",
                newName: "ix_school_memberships_member_type_id");

            migrationBuilder.RenameColumn(
                name: "VerifiedAtUtc",
                schema: "per",
                table: "person_identity_documents",
                newName: "verified_at_utc");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                schema: "per",
                table: "person_identity_documents",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "LookupHmac",
                schema: "per",
                table: "person_identity_documents",
                newName: "lookup_hmac");

            migrationBuilder.RenameColumn(
                name: "LastFour",
                schema: "per",
                table: "person_identity_documents",
                newName: "last_four");

            migrationBuilder.RenameColumn(
                name: "IdentityDocumentTypeId",
                schema: "per",
                table: "person_identity_documents",
                newName: "identity_document_type_id");

            migrationBuilder.RenameColumn(
                name: "EncryptedIdentifier",
                schema: "per",
                table: "person_identity_documents",
                newName: "encrypted_identifier");

            migrationBuilder.RenameColumn(
                name: "PersonIdentityDocumentId",
                schema: "per",
                table: "person_identity_documents",
                newName: "person_identity_document_id");

            migrationBuilder.RenameIndex(
                name: "IX_PersonIdentityDocuments_PersonId",
                schema: "per",
                table: "person_identity_documents",
                newName: "ix_person_identity_documents_person_id");

            migrationBuilder.RenameIndex(
                name: "IX_PersonIdentityDocuments_IdentityDocumentTypeId_LookupHmac",
                schema: "per",
                table: "person_identity_documents",
                newName: "ix_person_identity_documents_identity_document_type_id_lookup_");

            migrationBuilder.RenameColumn(
                name: "Value",
                schema: "per",
                table: "person_contacts",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                schema: "per",
                table: "person_contacts",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "IsVerified",
                schema: "per",
                table: "person_contacts",
                newName: "is_verified");

            migrationBuilder.RenameColumn(
                name: "IsPrimary",
                schema: "per",
                table: "person_contacts",
                newName: "is_primary");

            migrationBuilder.RenameColumn(
                name: "ContactTypeId",
                schema: "per",
                table: "person_contacts",
                newName: "contact_type_id");

            migrationBuilder.RenameColumn(
                name: "PersonContactId",
                schema: "per",
                table: "person_contacts",
                newName: "person_contact_id");

            migrationBuilder.RenameIndex(
                name: "IX_PersonContacts_PersonId",
                schema: "per",
                table: "person_contacts",
                newName: "ix_person_contacts_person_id");

            migrationBuilder.RenameIndex(
                name: "IX_PersonContacts_ContactTypeId",
                schema: "per",
                table: "person_contacts",
                newName: "ix_person_contacts_contact_type_id");

            migrationBuilder.RenameColumn(
                name: "City",
                schema: "per",
                table: "person_addresses",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "StateOrProvince",
                schema: "per",
                table: "person_addresses",
                newName: "state_or_province");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                schema: "per",
                table: "person_addresses",
                newName: "postal_code");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                schema: "per",
                table: "person_addresses",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "IsPrimary",
                schema: "per",
                table: "person_addresses",
                newName: "is_primary");

            migrationBuilder.RenameColumn(
                name: "CountryCode",
                schema: "per",
                table: "person_addresses",
                newName: "country_code");

            migrationBuilder.RenameColumn(
                name: "AddressTypeId",
                schema: "per",
                table: "person_addresses",
                newName: "address_type_id");

            migrationBuilder.RenameColumn(
                name: "AddressLine2",
                schema: "per",
                table: "person_addresses",
                newName: "address_line2");

            migrationBuilder.RenameColumn(
                name: "AddressLine1",
                schema: "per",
                table: "person_addresses",
                newName: "address_line1");

            migrationBuilder.RenameColumn(
                name: "PersonAddressId",
                schema: "per",
                table: "person_addresses",
                newName: "person_address_id");

            migrationBuilder.RenameIndex(
                name: "IX_PersonAddresses_PersonId",
                schema: "per",
                table: "person_addresses",
                newName: "ix_person_addresses_person_id");

            migrationBuilder.RenameIndex(
                name: "IX_PersonAddresses_AddressTypeId",
                schema: "per",
                table: "person_addresses",
                newName: "ix_person_addresses_address_type_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "sm",
                table: "member_types",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "sm",
                table: "member_types",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "sm",
                table: "member_types",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "RoleTypeId",
                schema: "sm",
                table: "member_types",
                newName: "member_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_RoleTypes_Code",
                schema: "sm",
                table: "member_types",
                newName: "ix_member_types_code");

            migrationBuilder.RenameColumn(
                name: "MedicalNotes",
                schema: "sd",
                table: "member_medical_profiles",
                newName: "medical_notes");

            migrationBuilder.RenameColumn(
                name: "BloodGroupId",
                schema: "sd",
                table: "member_medical_profiles",
                newName: "blood_group_id");

            migrationBuilder.RenameColumn(
                name: "SchoolMembershipId",
                schema: "sd",
                table: "member_medical_profiles",
                newName: "school_membership_id");

            migrationBuilder.RenameIndex(
                name: "IX_MemberMedicalProfiles_BloodGroupId",
                schema: "sd",
                table: "member_medical_profiles",
                newName: "ix_member_medical_profiles_blood_group_id");

            migrationBuilder.RenameColumn(
                name: "VerifiedAtUtc",
                schema: "sd",
                table: "member_documents",
                newName: "verified_at_utc");

            migrationBuilder.RenameColumn(
                name: "UploadedBySubject",
                schema: "sd",
                table: "member_documents",
                newName: "uploaded_by_subject");

            migrationBuilder.RenameColumn(
                name: "UploadedAtUtc",
                schema: "sd",
                table: "member_documents",
                newName: "uploaded_at_utc");

            migrationBuilder.RenameColumn(
                name: "StorageKey",
                schema: "sd",
                table: "member_documents",
                newName: "storage_key");

            migrationBuilder.RenameColumn(
                name: "SizeBytes",
                schema: "sd",
                table: "member_documents",
                newName: "size_bytes");

            migrationBuilder.RenameColumn(
                name: "SchoolMembershipId",
                schema: "sd",
                table: "member_documents",
                newName: "school_membership_id");

            migrationBuilder.RenameColumn(
                name: "FileName",
                schema: "sd",
                table: "member_documents",
                newName: "file_name");

            migrationBuilder.RenameColumn(
                name: "ExpiresAtUtc",
                schema: "sd",
                table: "member_documents",
                newName: "expires_at_utc");

            migrationBuilder.RenameColumn(
                name: "DocumentTypeId",
                schema: "sd",
                table: "member_documents",
                newName: "document_type_id");

            migrationBuilder.RenameColumn(
                name: "ContentType",
                schema: "sd",
                table: "member_documents",
                newName: "content_type");

            migrationBuilder.RenameColumn(
                name: "ContentHash",
                schema: "sd",
                table: "member_documents",
                newName: "content_hash");

            migrationBuilder.RenameColumn(
                name: "MemberDocumentId",
                schema: "sd",
                table: "member_documents",
                newName: "member_document_id");

            migrationBuilder.RenameIndex(
                name: "IX_MemberDocuments_SchoolMembershipId",
                schema: "sd",
                table: "member_documents",
                newName: "ix_member_documents_school_membership_id");

            migrationBuilder.RenameIndex(
                name: "IX_MemberDocuments_DocumentTypeId",
                schema: "sd",
                table: "member_documents",
                newName: "ix_member_documents_document_type_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "sm",
                table: "identity_document_types",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "sm",
                table: "identity_document_types",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "IdentityDocumentTypeId",
                schema: "sm",
                table: "identity_document_types",
                newName: "identity_document_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityDocumentTypes_Code",
                schema: "sm",
                table: "identity_document_types",
                newName: "ix_identity_document_types_code");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "sm",
                table: "guardian_relationship_types",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "sm",
                table: "guardian_relationship_types",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "GuardianTypeId",
                schema: "sm",
                table: "guardian_relationship_types",
                newName: "guardian_relationship_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_GuardianTypes_Code",
                schema: "sm",
                table: "guardian_relationship_types",
                newName: "ix_guardian_relationship_types_code");

            migrationBuilder.RenameColumn(
                name: "StudentMembershipId",
                schema: "sd",
                table: "guardian_relationships",
                newName: "student_membership_id");

            migrationBuilder.RenameColumn(
                name: "IsPrimary",
                schema: "sd",
                table: "guardian_relationships",
                newName: "is_primary");

            migrationBuilder.RenameColumn(
                name: "IsAuthorizedForPickup",
                schema: "sd",
                table: "guardian_relationships",
                newName: "is_authorized_for_pickup");

            migrationBuilder.RenameColumn(
                name: "GuardianTypeId",
                schema: "sd",
                table: "guardian_relationships",
                newName: "guardian_relationship_type_id");

            migrationBuilder.RenameColumn(
                name: "GuardianPersonId",
                schema: "sd",
                table: "guardian_relationships",
                newName: "guardian_person_id");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                schema: "sd",
                table: "guardian_relationships",
                newName: "end_date");

            migrationBuilder.RenameColumn(
                name: "CanReceiveCommunications",
                schema: "sd",
                table: "guardian_relationships",
                newName: "can_receive_communications");

            migrationBuilder.RenameColumn(
                name: "GuardianId",
                schema: "sd",
                table: "guardian_relationships",
                newName: "guardian_relationship_id");

            migrationBuilder.RenameIndex(
                name: "IX_Guardians_StudentMembershipId_GuardianPersonId",
                schema: "sd",
                table: "guardian_relationships",
                newName: "ix_guardian_relationships_student_membership_id_guardian_perso");

            migrationBuilder.RenameIndex(
                name: "IX_Guardians_GuardianTypeId",
                schema: "sd",
                table: "guardian_relationships",
                newName: "ix_guardian_relationships_guardian_relationship_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_Guardians_GuardianPersonId",
                schema: "sd",
                table: "guardian_relationships",
                newName: "ix_guardian_relationships_guardian_person_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "sm",
                table: "document_types",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "sm",
                table: "document_types",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "sm",
                table: "document_types",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "DocumentTypeId",
                schema: "sm",
                table: "document_types",
                newName: "document_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentTypes_Code",
                schema: "sm",
                table: "document_types",
                newName: "ix_document_types_code");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "sm",
                table: "contact_types",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "sm",
                table: "contact_types",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "ContactTypeId",
                schema: "sm",
                table: "contact_types",
                newName: "contact_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_ContactTypes_Code",
                schema: "sm",
                table: "contact_types",
                newName: "ix_contact_types_code");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "sm",
                table: "blood_groups",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "sm",
                table: "blood_groups",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "BloodGroupId",
                schema: "sm",
                table: "blood_groups",
                newName: "blood_group_id");

            migrationBuilder.RenameIndex(
                name: "IX_BloodGroups_Code",
                schema: "sm",
                table: "blood_groups",
                newName: "ix_blood_groups_code");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "sm",
                table: "address_types",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "sm",
                table: "address_types",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "AddressTypeId",
                schema: "sm",
                table: "address_types",
                newName: "address_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_AddressTypes_Code",
                schema: "sm",
                table: "address_types",
                newName: "ix_address_types_code");

            migrationBuilder.AddPrimaryKey(
                name: "pk_schools",
                schema: "sm",
                table: "schools",
                column: "school_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_people",
                schema: "per",
                table: "people",
                column: "person_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_student_previous_schools",
                schema: "sd",
                table: "student_previous_schools",
                column: "student_previous_school_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_school_memberships",
                schema: "sd",
                table: "school_memberships",
                column: "school_membership_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_person_identity_documents",
                schema: "per",
                table: "person_identity_documents",
                column: "person_identity_document_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_person_contacts",
                schema: "per",
                table: "person_contacts",
                column: "person_contact_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_person_addresses",
                schema: "per",
                table: "person_addresses",
                column: "person_address_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_member_types",
                schema: "sm",
                table: "member_types",
                column: "member_type_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_member_medical_profiles",
                schema: "sd",
                table: "member_medical_profiles",
                column: "school_membership_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_member_documents",
                schema: "sd",
                table: "member_documents",
                column: "member_document_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_identity_document_types",
                schema: "sm",
                table: "identity_document_types",
                column: "identity_document_type_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_guardian_relationship_types",
                schema: "sm",
                table: "guardian_relationship_types",
                column: "guardian_relationship_type_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_guardian_relationships",
                schema: "sd",
                table: "guardian_relationships",
                column: "guardian_relationship_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_document_types",
                schema: "sm",
                table: "document_types",
                column: "document_type_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_contact_types",
                schema: "sm",
                table: "contact_types",
                column: "contact_type_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_blood_groups",
                schema: "sm",
                table: "blood_groups",
                column: "blood_group_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_address_types",
                schema: "sm",
                table: "address_types",
                column: "address_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_guardian_relationships_guardian_relationship_types_guardian",
                schema: "sd",
                table: "guardian_relationships",
                column: "guardian_relationship_type_id",
                principalSchema: "sm",
                principalTable: "guardian_relationship_types",
                principalColumn: "guardian_relationship_type_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_guardian_relationships_people_guardian_person_id",
                schema: "sd",
                table: "guardian_relationships",
                column: "guardian_person_id",
                principalSchema: "per",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_guardian_relationships_school_memberships_student_membershi",
                schema: "sd",
                table: "guardian_relationships",
                column: "student_membership_id",
                principalSchema: "sd",
                principalTable: "school_memberships",
                principalColumn: "school_membership_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_member_documents_document_types_document_type_id",
                schema: "sd",
                table: "member_documents",
                column: "document_type_id",
                principalSchema: "sm",
                principalTable: "document_types",
                principalColumn: "document_type_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_member_documents_school_memberships_school_membership_id",
                schema: "sd",
                table: "member_documents",
                column: "school_membership_id",
                principalSchema: "sd",
                principalTable: "school_memberships",
                principalColumn: "school_membership_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_member_medical_profiles_blood_groups_blood_group_id",
                schema: "sd",
                table: "member_medical_profiles",
                column: "blood_group_id",
                principalSchema: "sm",
                principalTable: "blood_groups",
                principalColumn: "blood_group_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_member_medical_profiles_school_memberships_school_membershi",
                schema: "sd",
                table: "member_medical_profiles",
                column: "school_membership_id",
                principalSchema: "sd",
                principalTable: "school_memberships",
                principalColumn: "school_membership_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_addresses_address_types_address_type_id",
                schema: "per",
                table: "person_addresses",
                column: "address_type_id",
                principalSchema: "sm",
                principalTable: "address_types",
                principalColumn: "address_type_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_addresses_people_person_id",
                schema: "per",
                table: "person_addresses",
                column: "person_id",
                principalSchema: "per",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_contacts_contact_types_contact_type_id",
                schema: "per",
                table: "person_contacts",
                column: "contact_type_id",
                principalSchema: "sm",
                principalTable: "contact_types",
                principalColumn: "contact_type_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_contacts_people_person_id",
                schema: "per",
                table: "person_contacts",
                column: "person_id",
                principalSchema: "per",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_identity_documents_identity_document_types_identity_",
                schema: "per",
                table: "person_identity_documents",
                column: "identity_document_type_id",
                principalSchema: "sm",
                principalTable: "identity_document_types",
                principalColumn: "identity_document_type_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_identity_documents_people_person_id",
                schema: "per",
                table: "person_identity_documents",
                column: "person_id",
                principalSchema: "per",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_school_memberships_member_types_member_type_id",
                schema: "sd",
                table: "school_memberships",
                column: "member_type_id",
                principalSchema: "sm",
                principalTable: "member_types",
                principalColumn: "member_type_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_school_memberships_people_person_id",
                schema: "sd",
                table: "school_memberships",
                column: "person_id",
                principalSchema: "per",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_school_memberships_schools_school_id",
                schema: "sd",
                table: "school_memberships",
                column: "school_id",
                principalSchema: "sm",
                principalTable: "schools",
                principalColumn: "school_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_student_previous_schools_school_memberships_school_membersh",
                schema: "sd",
                table: "student_previous_schools",
                column: "school_membership_id",
                principalSchema: "sd",
                principalTable: "school_memberships",
                principalColumn: "school_membership_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_guardian_relationships_guardian_relationship_types_guardian",
                schema: "sd",
                table: "guardian_relationships");

            migrationBuilder.DropForeignKey(
                name: "fk_guardian_relationships_people_guardian_person_id",
                schema: "sd",
                table: "guardian_relationships");

            migrationBuilder.DropForeignKey(
                name: "fk_guardian_relationships_school_memberships_student_membershi",
                schema: "sd",
                table: "guardian_relationships");

            migrationBuilder.DropForeignKey(
                name: "fk_member_documents_document_types_document_type_id",
                schema: "sd",
                table: "member_documents");

            migrationBuilder.DropForeignKey(
                name: "fk_member_documents_school_memberships_school_membership_id",
                schema: "sd",
                table: "member_documents");

            migrationBuilder.DropForeignKey(
                name: "fk_member_medical_profiles_blood_groups_blood_group_id",
                schema: "sd",
                table: "member_medical_profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_member_medical_profiles_school_memberships_school_membershi",
                schema: "sd",
                table: "member_medical_profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_person_addresses_address_types_address_type_id",
                schema: "per",
                table: "person_addresses");

            migrationBuilder.DropForeignKey(
                name: "fk_person_addresses_people_person_id",
                schema: "per",
                table: "person_addresses");

            migrationBuilder.DropForeignKey(
                name: "fk_person_contacts_contact_types_contact_type_id",
                schema: "per",
                table: "person_contacts");

            migrationBuilder.DropForeignKey(
                name: "fk_person_contacts_people_person_id",
                schema: "per",
                table: "person_contacts");

            migrationBuilder.DropForeignKey(
                name: "fk_person_identity_documents_identity_document_types_identity_",
                schema: "per",
                table: "person_identity_documents");

            migrationBuilder.DropForeignKey(
                name: "fk_person_identity_documents_people_person_id",
                schema: "per",
                table: "person_identity_documents");

            migrationBuilder.DropForeignKey(
                name: "fk_school_memberships_member_types_member_type_id",
                schema: "sd",
                table: "school_memberships");

            migrationBuilder.DropForeignKey(
                name: "fk_school_memberships_people_person_id",
                schema: "sd",
                table: "school_memberships");

            migrationBuilder.DropForeignKey(
                name: "fk_school_memberships_schools_school_id",
                schema: "sd",
                table: "school_memberships");

            migrationBuilder.DropForeignKey(
                name: "fk_student_previous_schools_school_memberships_school_membersh",
                schema: "sd",
                table: "student_previous_schools");

            migrationBuilder.DropPrimaryKey(
                name: "pk_schools",
                schema: "sm",
                table: "schools");

            migrationBuilder.DropPrimaryKey(
                name: "pk_people",
                schema: "per",
                table: "people");

            migrationBuilder.DropPrimaryKey(
                name: "pk_student_previous_schools",
                schema: "sd",
                table: "student_previous_schools");

            migrationBuilder.DropPrimaryKey(
                name: "pk_school_memberships",
                schema: "sd",
                table: "school_memberships");

            migrationBuilder.DropPrimaryKey(
                name: "pk_person_identity_documents",
                schema: "per",
                table: "person_identity_documents");

            migrationBuilder.DropPrimaryKey(
                name: "pk_person_contacts",
                schema: "per",
                table: "person_contacts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_person_addresses",
                schema: "per",
                table: "person_addresses");

            migrationBuilder.DropPrimaryKey(
                name: "pk_member_types",
                schema: "sm",
                table: "member_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_member_medical_profiles",
                schema: "sd",
                table: "member_medical_profiles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_member_documents",
                schema: "sd",
                table: "member_documents");

            migrationBuilder.DropPrimaryKey(
                name: "pk_identity_document_types",
                schema: "sm",
                table: "identity_document_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_guardian_relationships",
                schema: "sd",
                table: "guardian_relationships");

            migrationBuilder.DropPrimaryKey(
                name: "pk_guardian_relationship_types",
                schema: "sm",
                table: "guardian_relationship_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_document_types",
                schema: "sm",
                table: "document_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_contact_types",
                schema: "sm",
                table: "contact_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_blood_groups",
                schema: "sm",
                table: "blood_groups");

            migrationBuilder.DropPrimaryKey(
                name: "pk_address_types",
                schema: "sm",
                table: "address_types");

            migrationBuilder.RenameTable(
                name: "schools",
                schema: "sm",
                newName: "Schools",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "people",
                schema: "per",
                newName: "People",
                newSchema: "per");

            migrationBuilder.RenameTable(
                name: "student_previous_schools",
                schema: "sd",
                newName: "StudentPreviousSchools",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "school_memberships",
                schema: "sd",
                newName: "SchoolMemberships",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "person_identity_documents",
                schema: "per",
                newName: "PersonIdentityDocuments",
                newSchema: "per");

            migrationBuilder.RenameTable(
                name: "person_contacts",
                schema: "per",
                newName: "PersonContacts",
                newSchema: "per");

            migrationBuilder.RenameTable(
                name: "person_addresses",
                schema: "per",
                newName: "PersonAddresses",
                newSchema: "per");

            migrationBuilder.RenameTable(
                name: "member_types",
                schema: "sm",
                newName: "RoleTypes",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "member_medical_profiles",
                schema: "sd",
                newName: "MemberMedicalProfiles",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "member_documents",
                schema: "sd",
                newName: "MemberDocuments",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "identity_document_types",
                schema: "sm",
                newName: "IdentityDocumentTypes",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "guardian_relationships",
                schema: "sd",
                newName: "Guardians",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "guardian_relationship_types",
                schema: "sm",
                newName: "GuardianTypes",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "document_types",
                schema: "sm",
                newName: "DocumentTypes",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "contact_types",
                schema: "sm",
                newName: "ContactTypes",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "blood_groups",
                schema: "sm",
                newName: "BloodGroups",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "address_types",
                schema: "sm",
                newName: "AddressTypes",
                newSchema: "sm");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "sm",
                table: "Schools",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "sm",
                table: "Schools",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "school_id",
                schema: "sm",
                table: "Schools",
                newName: "SchoolId");

            migrationBuilder.RenameColumn(
                name: "middle_name",
                schema: "per",
                table: "People",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "last_name",
                schema: "per",
                table: "People",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "identity_user_id",
                schema: "per",
                table: "People",
                newName: "IdentityUserId");

            migrationBuilder.RenameColumn(
                name: "first_name",
                schema: "per",
                table: "People",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "person_id",
                schema: "per",
                table: "People",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "ix_people_identity_user_id",
                schema: "per",
                table: "People",
                newName: "IX_People_IdentityUserId");

            migrationBuilder.RenameColumn(
                name: "city",
                schema: "sd",
                table: "StudentPreviousSchools",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "start_date",
                schema: "sd",
                table: "StudentPreviousSchools",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "school_membership_id",
                schema: "sd",
                table: "StudentPreviousSchools",
                newName: "SchoolMembershipId");

            migrationBuilder.RenameColumn(
                name: "last_grade",
                schema: "sd",
                table: "StudentPreviousSchools",
                newName: "LastGrade");

            migrationBuilder.RenameColumn(
                name: "institution_name",
                schema: "sd",
                table: "StudentPreviousSchools",
                newName: "InstitutionName");

            migrationBuilder.RenameColumn(
                name: "end_date",
                schema: "sd",
                table: "StudentPreviousSchools",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "country_code",
                schema: "sd",
                table: "StudentPreviousSchools",
                newName: "CountryCode");

            migrationBuilder.RenameColumn(
                name: "student_previous_school_id",
                schema: "sd",
                table: "StudentPreviousSchools",
                newName: "StudentPreviousSchoolId");

            migrationBuilder.RenameIndex(
                name: "ix_student_previous_schools_school_membership_id",
                schema: "sd",
                table: "StudentPreviousSchools",
                newName: "IX_StudentPreviousSchools_SchoolMembershipId");

            migrationBuilder.RenameColumn(
                name: "updated_by_subject",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at_utc",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "UpdatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "start_date",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "school_id",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "SchoolId");

            migrationBuilder.RenameColumn(
                name: "person_id",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "member_type_id",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "RoleTypeId");

            migrationBuilder.RenameColumn(
                name: "member_number",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "MemberNumber");

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "end_date",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "created_by_subject",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at_utc",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "school_membership_id",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "SchoolMembershipId");

            migrationBuilder.RenameIndex(
                name: "ix_school_memberships_school_id_member_number",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "IX_SchoolMemberships_SchoolId_MemberNumber");

            migrationBuilder.RenameIndex(
                name: "ix_school_memberships_person_id",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "IX_SchoolMemberships_PersonId");

            migrationBuilder.RenameIndex(
                name: "ix_school_memberships_member_type_id",
                schema: "sd",
                table: "SchoolMemberships",
                newName: "IX_SchoolMemberships_RoleTypeId");

            migrationBuilder.RenameColumn(
                name: "verified_at_utc",
                schema: "per",
                table: "PersonIdentityDocuments",
                newName: "VerifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "person_id",
                schema: "per",
                table: "PersonIdentityDocuments",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "lookup_hmac",
                schema: "per",
                table: "PersonIdentityDocuments",
                newName: "LookupHmac");

            migrationBuilder.RenameColumn(
                name: "last_four",
                schema: "per",
                table: "PersonIdentityDocuments",
                newName: "LastFour");

            migrationBuilder.RenameColumn(
                name: "identity_document_type_id",
                schema: "per",
                table: "PersonIdentityDocuments",
                newName: "IdentityDocumentTypeId");

            migrationBuilder.RenameColumn(
                name: "encrypted_identifier",
                schema: "per",
                table: "PersonIdentityDocuments",
                newName: "EncryptedIdentifier");

            migrationBuilder.RenameColumn(
                name: "person_identity_document_id",
                schema: "per",
                table: "PersonIdentityDocuments",
                newName: "PersonIdentityDocumentId");

            migrationBuilder.RenameIndex(
                name: "ix_person_identity_documents_person_id",
                schema: "per",
                table: "PersonIdentityDocuments",
                newName: "IX_PersonIdentityDocuments_PersonId");

            migrationBuilder.RenameIndex(
                name: "ix_person_identity_documents_identity_document_type_id_lookup_",
                schema: "per",
                table: "PersonIdentityDocuments",
                newName: "IX_PersonIdentityDocuments_IdentityDocumentTypeId_LookupHmac");

            migrationBuilder.RenameColumn(
                name: "value",
                schema: "per",
                table: "PersonContacts",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "person_id",
                schema: "per",
                table: "PersonContacts",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "is_verified",
                schema: "per",
                table: "PersonContacts",
                newName: "IsVerified");

            migrationBuilder.RenameColumn(
                name: "is_primary",
                schema: "per",
                table: "PersonContacts",
                newName: "IsPrimary");

            migrationBuilder.RenameColumn(
                name: "contact_type_id",
                schema: "per",
                table: "PersonContacts",
                newName: "ContactTypeId");

            migrationBuilder.RenameColumn(
                name: "person_contact_id",
                schema: "per",
                table: "PersonContacts",
                newName: "PersonContactId");

            migrationBuilder.RenameIndex(
                name: "ix_person_contacts_person_id",
                schema: "per",
                table: "PersonContacts",
                newName: "IX_PersonContacts_PersonId");

            migrationBuilder.RenameIndex(
                name: "ix_person_contacts_contact_type_id",
                schema: "per",
                table: "PersonContacts",
                newName: "IX_PersonContacts_ContactTypeId");

            migrationBuilder.RenameColumn(
                name: "city",
                schema: "per",
                table: "PersonAddresses",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "state_or_province",
                schema: "per",
                table: "PersonAddresses",
                newName: "StateOrProvince");

            migrationBuilder.RenameColumn(
                name: "postal_code",
                schema: "per",
                table: "PersonAddresses",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "person_id",
                schema: "per",
                table: "PersonAddresses",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "is_primary",
                schema: "per",
                table: "PersonAddresses",
                newName: "IsPrimary");

            migrationBuilder.RenameColumn(
                name: "country_code",
                schema: "per",
                table: "PersonAddresses",
                newName: "CountryCode");

            migrationBuilder.RenameColumn(
                name: "address_type_id",
                schema: "per",
                table: "PersonAddresses",
                newName: "AddressTypeId");

            migrationBuilder.RenameColumn(
                name: "address_line2",
                schema: "per",
                table: "PersonAddresses",
                newName: "AddressLine2");

            migrationBuilder.RenameColumn(
                name: "address_line1",
                schema: "per",
                table: "PersonAddresses",
                newName: "AddressLine1");

            migrationBuilder.RenameColumn(
                name: "person_address_id",
                schema: "per",
                table: "PersonAddresses",
                newName: "PersonAddressId");

            migrationBuilder.RenameIndex(
                name: "ix_person_addresses_person_id",
                schema: "per",
                table: "PersonAddresses",
                newName: "IX_PersonAddresses_PersonId");

            migrationBuilder.RenameIndex(
                name: "ix_person_addresses_address_type_id",
                schema: "per",
                table: "PersonAddresses",
                newName: "IX_PersonAddresses_AddressTypeId");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "sm",
                table: "RoleTypes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "code",
                schema: "sm",
                table: "RoleTypes",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "sm",
                table: "RoleTypes",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "member_type_id",
                schema: "sm",
                table: "RoleTypes",
                newName: "RoleTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_member_types_code",
                schema: "sm",
                table: "RoleTypes",
                newName: "IX_RoleTypes_Code");

            migrationBuilder.RenameColumn(
                name: "medical_notes",
                schema: "sd",
                table: "MemberMedicalProfiles",
                newName: "MedicalNotes");

            migrationBuilder.RenameColumn(
                name: "blood_group_id",
                schema: "sd",
                table: "MemberMedicalProfiles",
                newName: "BloodGroupId");

            migrationBuilder.RenameColumn(
                name: "school_membership_id",
                schema: "sd",
                table: "MemberMedicalProfiles",
                newName: "SchoolMembershipId");

            migrationBuilder.RenameIndex(
                name: "ix_member_medical_profiles_blood_group_id",
                schema: "sd",
                table: "MemberMedicalProfiles",
                newName: "IX_MemberMedicalProfiles_BloodGroupId");

            migrationBuilder.RenameColumn(
                name: "verified_at_utc",
                schema: "sd",
                table: "MemberDocuments",
                newName: "VerifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "uploaded_by_subject",
                schema: "sd",
                table: "MemberDocuments",
                newName: "UploadedBySubject");

            migrationBuilder.RenameColumn(
                name: "uploaded_at_utc",
                schema: "sd",
                table: "MemberDocuments",
                newName: "UploadedAtUtc");

            migrationBuilder.RenameColumn(
                name: "storage_key",
                schema: "sd",
                table: "MemberDocuments",
                newName: "StorageKey");

            migrationBuilder.RenameColumn(
                name: "size_bytes",
                schema: "sd",
                table: "MemberDocuments",
                newName: "SizeBytes");

            migrationBuilder.RenameColumn(
                name: "school_membership_id",
                schema: "sd",
                table: "MemberDocuments",
                newName: "SchoolMembershipId");

            migrationBuilder.RenameColumn(
                name: "file_name",
                schema: "sd",
                table: "MemberDocuments",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "expires_at_utc",
                schema: "sd",
                table: "MemberDocuments",
                newName: "ExpiresAtUtc");

            migrationBuilder.RenameColumn(
                name: "document_type_id",
                schema: "sd",
                table: "MemberDocuments",
                newName: "DocumentTypeId");

            migrationBuilder.RenameColumn(
                name: "content_type",
                schema: "sd",
                table: "MemberDocuments",
                newName: "ContentType");

            migrationBuilder.RenameColumn(
                name: "content_hash",
                schema: "sd",
                table: "MemberDocuments",
                newName: "ContentHash");

            migrationBuilder.RenameColumn(
                name: "member_document_id",
                schema: "sd",
                table: "MemberDocuments",
                newName: "MemberDocumentId");

            migrationBuilder.RenameIndex(
                name: "ix_member_documents_school_membership_id",
                schema: "sd",
                table: "MemberDocuments",
                newName: "IX_MemberDocuments_SchoolMembershipId");

            migrationBuilder.RenameIndex(
                name: "ix_member_documents_document_type_id",
                schema: "sd",
                table: "MemberDocuments",
                newName: "IX_MemberDocuments_DocumentTypeId");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "sm",
                table: "IdentityDocumentTypes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "code",
                schema: "sm",
                table: "IdentityDocumentTypes",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "identity_document_type_id",
                schema: "sm",
                table: "IdentityDocumentTypes",
                newName: "IdentityDocumentTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_identity_document_types_code",
                schema: "sm",
                table: "IdentityDocumentTypes",
                newName: "IX_IdentityDocumentTypes_Code");

            migrationBuilder.RenameColumn(
                name: "student_membership_id",
                schema: "sd",
                table: "Guardians",
                newName: "StudentMembershipId");

            migrationBuilder.RenameColumn(
                name: "is_primary",
                schema: "sd",
                table: "Guardians",
                newName: "IsPrimary");

            migrationBuilder.RenameColumn(
                name: "is_authorized_for_pickup",
                schema: "sd",
                table: "Guardians",
                newName: "IsAuthorizedForPickup");

            migrationBuilder.RenameColumn(
                name: "guardian_relationship_type_id",
                schema: "sd",
                table: "Guardians",
                newName: "GuardianTypeId");

            migrationBuilder.RenameColumn(
                name: "guardian_person_id",
                schema: "sd",
                table: "Guardians",
                newName: "GuardianPersonId");

            migrationBuilder.RenameColumn(
                name: "end_date",
                schema: "sd",
                table: "Guardians",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "can_receive_communications",
                schema: "sd",
                table: "Guardians",
                newName: "CanReceiveCommunications");

            migrationBuilder.RenameColumn(
                name: "guardian_relationship_id",
                schema: "sd",
                table: "Guardians",
                newName: "GuardianId");

            migrationBuilder.RenameIndex(
                name: "ix_guardian_relationships_student_membership_id_guardian_perso",
                schema: "sd",
                table: "Guardians",
                newName: "IX_Guardians_StudentMembershipId_GuardianPersonId");

            migrationBuilder.RenameIndex(
                name: "ix_guardian_relationships_guardian_relationship_type_id",
                schema: "sd",
                table: "Guardians",
                newName: "IX_Guardians_GuardianTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_guardian_relationships_guardian_person_id",
                schema: "sd",
                table: "Guardians",
                newName: "IX_Guardians_GuardianPersonId");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "sm",
                table: "GuardianTypes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "code",
                schema: "sm",
                table: "GuardianTypes",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "guardian_relationship_type_id",
                schema: "sm",
                table: "GuardianTypes",
                newName: "GuardianTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_guardian_relationship_types_code",
                schema: "sm",
                table: "GuardianTypes",
                newName: "IX_GuardianTypes_Code");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "sm",
                table: "DocumentTypes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "code",
                schema: "sm",
                table: "DocumentTypes",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "sm",
                table: "DocumentTypes",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "document_type_id",
                schema: "sm",
                table: "DocumentTypes",
                newName: "DocumentTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_document_types_code",
                schema: "sm",
                table: "DocumentTypes",
                newName: "IX_DocumentTypes_Code");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "sm",
                table: "ContactTypes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "code",
                schema: "sm",
                table: "ContactTypes",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "contact_type_id",
                schema: "sm",
                table: "ContactTypes",
                newName: "ContactTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_contact_types_code",
                schema: "sm",
                table: "ContactTypes",
                newName: "IX_ContactTypes_Code");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "sm",
                table: "BloodGroups",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "code",
                schema: "sm",
                table: "BloodGroups",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "blood_group_id",
                schema: "sm",
                table: "BloodGroups",
                newName: "BloodGroupId");

            migrationBuilder.RenameIndex(
                name: "ix_blood_groups_code",
                schema: "sm",
                table: "BloodGroups",
                newName: "IX_BloodGroups_Code");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "sm",
                table: "AddressTypes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "code",
                schema: "sm",
                table: "AddressTypes",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "address_type_id",
                schema: "sm",
                table: "AddressTypes",
                newName: "AddressTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_address_types_code",
                schema: "sm",
                table: "AddressTypes",
                newName: "IX_AddressTypes_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schools",
                schema: "sm",
                table: "Schools",
                column: "SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                schema: "per",
                table: "People",
                column: "PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentPreviousSchools",
                schema: "sd",
                table: "StudentPreviousSchools",
                column: "StudentPreviousSchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolMemberships",
                schema: "sd",
                table: "SchoolMemberships",
                column: "SchoolMembershipId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonIdentityDocuments",
                schema: "per",
                table: "PersonIdentityDocuments",
                column: "PersonIdentityDocumentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonContacts",
                schema: "per",
                table: "PersonContacts",
                column: "PersonContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonAddresses",
                schema: "per",
                table: "PersonAddresses",
                column: "PersonAddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleTypes",
                schema: "sm",
                table: "RoleTypes",
                column: "RoleTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberMedicalProfiles",
                schema: "sd",
                table: "MemberMedicalProfiles",
                column: "SchoolMembershipId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberDocuments",
                schema: "sd",
                table: "MemberDocuments",
                column: "MemberDocumentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityDocumentTypes",
                schema: "sm",
                table: "IdentityDocumentTypes",
                column: "IdentityDocumentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guardians",
                schema: "sd",
                table: "Guardians",
                column: "GuardianId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuardianTypes",
                schema: "sm",
                table: "GuardianTypes",
                column: "GuardianTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentTypes",
                schema: "sm",
                table: "DocumentTypes",
                column: "DocumentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactTypes",
                schema: "sm",
                table: "ContactTypes",
                column: "ContactTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BloodGroups",
                schema: "sm",
                table: "BloodGroups",
                column: "BloodGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressTypes",
                schema: "sm",
                table: "AddressTypes",
                column: "AddressTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guardians_GuardianTypes_GuardianRel~",
                schema: "sd",
                table: "Guardians",
                column: "GuardianTypeId",
                principalSchema: "sm",
                principalTable: "GuardianTypes",
                principalColumn: "GuardianTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guardians_People_GuardianPersonId",
                schema: "sd",
                table: "Guardians",
                column: "GuardianPersonId",
                principalSchema: "per",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guardians_SchoolMemberships_StudentMembershipId",
                schema: "sd",
                table: "Guardians",
                column: "StudentMembershipId",
                principalSchema: "sd",
                principalTable: "SchoolMemberships",
                principalColumn: "SchoolMembershipId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberDocuments_DocumentTypes_DocumentTypeId",
                schema: "sd",
                table: "MemberDocuments",
                column: "DocumentTypeId",
                principalSchema: "sm",
                principalTable: "DocumentTypes",
                principalColumn: "DocumentTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberDocuments_SchoolMemberships_SchoolMembershipId",
                schema: "sd",
                table: "MemberDocuments",
                column: "SchoolMembershipId",
                principalSchema: "sd",
                principalTable: "SchoolMemberships",
                principalColumn: "SchoolMembershipId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberMedicalProfiles_BloodGroups_BloodGroupId",
                schema: "sd",
                table: "MemberMedicalProfiles",
                column: "BloodGroupId",
                principalSchema: "sm",
                principalTable: "BloodGroups",
                principalColumn: "BloodGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberMedicalProfiles_SchoolMemberships_SchoolMembershipId",
                schema: "sd",
                table: "MemberMedicalProfiles",
                column: "SchoolMembershipId",
                principalSchema: "sd",
                principalTable: "SchoolMemberships",
                principalColumn: "SchoolMembershipId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonAddresses_AddressTypes_AddressTypeId",
                schema: "per",
                table: "PersonAddresses",
                column: "AddressTypeId",
                principalSchema: "sm",
                principalTable: "AddressTypes",
                principalColumn: "AddressTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonAddresses_People_PersonId",
                schema: "per",
                table: "PersonAddresses",
                column: "PersonId",
                principalSchema: "per",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonContacts_ContactTypes_ContactTypeId",
                schema: "per",
                table: "PersonContacts",
                column: "ContactTypeId",
                principalSchema: "sm",
                principalTable: "ContactTypes",
                principalColumn: "ContactTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonContacts_People_PersonId",
                schema: "per",
                table: "PersonContacts",
                column: "PersonId",
                principalSchema: "per",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonIdentityDocuments_IdentityDocumentTypes_IdentityDocum~",
                schema: "per",
                table: "PersonIdentityDocuments",
                column: "IdentityDocumentTypeId",
                principalSchema: "sm",
                principalTable: "IdentityDocumentTypes",
                principalColumn: "IdentityDocumentTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonIdentityDocuments_People_PersonId",
                schema: "per",
                table: "PersonIdentityDocuments",
                column: "PersonId",
                principalSchema: "per",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolMemberships_RoleTypes_RoleTypeId",
                schema: "sd",
                table: "SchoolMemberships",
                column: "RoleTypeId",
                principalSchema: "sm",
                principalTable: "RoleTypes",
                principalColumn: "RoleTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolMemberships_People_PersonId",
                schema: "sd",
                table: "SchoolMemberships",
                column: "PersonId",
                principalSchema: "per",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolMemberships_Schools_SchoolId",
                schema: "sd",
                table: "SchoolMemberships",
                column: "SchoolId",
                principalSchema: "sm",
                principalTable: "Schools",
                principalColumn: "SchoolId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPreviousSchools_SchoolMemberships_SchoolMembershipId",
                schema: "sd",
                table: "StudentPreviousSchools",
                column: "SchoolMembershipId",
                principalSchema: "sd",
                principalTable: "SchoolMemberships",
                principalColumn: "SchoolMembershipId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MoveTablesToSchemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sm");

            migrationBuilder.EnsureSchema(
                name: "sd");

            migrationBuilder.EnsureSchema(
                name: "per");

            migrationBuilder.RenameTable(
                name: "StudentPreviousSchools",
                newName: "StudentPreviousSchools",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "Schools",
                newName: "Schools",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "SchoolMemberships",
                newName: "SchoolMemberships",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "PersonIdentityDocuments",
                newName: "PersonIdentityDocuments",
                newSchema: "per");

            migrationBuilder.RenameTable(
                name: "PersonContacts",
                newName: "PersonContacts",
                newSchema: "per");

            migrationBuilder.RenameTable(
                name: "PersonAddresses",
                newName: "PersonAddresses",
                newSchema: "per");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "People",
                newSchema: "per");

            migrationBuilder.RenameTable(
                name: "RoleTypes",
                newName: "RoleTypes",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "MemberMedicalProfiles",
                newName: "MemberMedicalProfiles",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "MemberDocuments",
                newName: "MemberDocuments",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "IdentityDocumentTypes",
                newName: "IdentityDocumentTypes",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "GuardianTypes",
                newName: "GuardianTypes",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "Guardians",
                newName: "Guardians",
                newSchema: "sd");

            migrationBuilder.RenameTable(
                name: "DocumentTypes",
                newName: "DocumentTypes",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "ContactTypes",
                newName: "ContactTypes",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "BloodGroups",
                newName: "BloodGroups",
                newSchema: "sm");

            migrationBuilder.RenameTable(
                name: "AddressTypes",
                newName: "AddressTypes",
                newSchema: "sm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "StudentPreviousSchools",
                schema: "sd",
                newName: "StudentPreviousSchools");

            migrationBuilder.RenameTable(
                name: "Schools",
                schema: "sm",
                newName: "Schools");

            migrationBuilder.RenameTable(
                name: "SchoolMemberships",
                schema: "sd",
                newName: "SchoolMemberships");

            migrationBuilder.RenameTable(
                name: "PersonIdentityDocuments",
                schema: "per",
                newName: "PersonIdentityDocuments");

            migrationBuilder.RenameTable(
                name: "PersonContacts",
                schema: "per",
                newName: "PersonContacts");

            migrationBuilder.RenameTable(
                name: "PersonAddresses",
                schema: "per",
                newName: "PersonAddresses");

            migrationBuilder.RenameTable(
                name: "People",
                schema: "per",
                newName: "People");

            migrationBuilder.RenameTable(
                name: "RoleTypes",
                schema: "sm",
                newName: "RoleTypes");

            migrationBuilder.RenameTable(
                name: "MemberMedicalProfiles",
                schema: "sd",
                newName: "MemberMedicalProfiles");

            migrationBuilder.RenameTable(
                name: "MemberDocuments",
                schema: "sd",
                newName: "MemberDocuments");

            migrationBuilder.RenameTable(
                name: "IdentityDocumentTypes",
                schema: "sm",
                newName: "IdentityDocumentTypes");

            migrationBuilder.RenameTable(
                name: "GuardianTypes",
                schema: "sm",
                newName: "GuardianTypes");

            migrationBuilder.RenameTable(
                name: "Guardians",
                schema: "sd",
                newName: "Guardians");

            migrationBuilder.RenameTable(
                name: "DocumentTypes",
                schema: "sm",
                newName: "DocumentTypes");

            migrationBuilder.RenameTable(
                name: "ContactTypes",
                schema: "sm",
                newName: "ContactTypes");

            migrationBuilder.RenameTable(
                name: "BloodGroups",
                schema: "sm",
                newName: "BloodGroups");

            migrationBuilder.RenameTable(
                name: "AddressTypes",
                schema: "sm",
                newName: "AddressTypes");
        }
    }
}

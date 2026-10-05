using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SchoolService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchoolSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressTypes",
                columns: table => new
                {
                    AddressTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressTypes", x => x.AddressTypeId);
                });

            migrationBuilder.CreateTable(
                name: "BloodGroups",
                columns: table => new
                {
                    BloodGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodGroups", x => x.BloodGroupId);
                });

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    ContactTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.ContactTypeId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    DocumentTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.DocumentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "GuardianTypes",
                columns: table => new
                {
                    GuardianTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuardianTypes", x => x.GuardianTypeId);
                });

            migrationBuilder.CreateTable(
                name: "IdentityDocumentTypes",
                columns: table => new
                {
                    IdentityDocumentTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityDocumentTypes", x => x.IdentityDocumentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RoleTypes",
                columns: table => new
                {
                    RoleTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypes", x => x.RoleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    SchoolId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.SchoolId);
                });

            migrationBuilder.CreateTable(
                name: "PersonAddresses",
                columns: table => new
                {
                    PersonAddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressTypeId = table.Column<int>(type: "integer", nullable: false),
                    AddressLine1 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AddressLine2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StateOrProvince = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CountryCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAddresses", x => x.PersonAddressId);
                    table.ForeignKey(
                        name: "FK_PersonAddresses_AddressTypes_AddressTypeId",
                        column: x => x.AddressTypeId,
                        principalTable: "AddressTypes",
                        principalColumn: "AddressTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonAddresses_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonContacts",
                columns: table => new
                {
                    PersonContactId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactTypeId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonContacts", x => x.PersonContactId);
                    table.ForeignKey(
                        name: "FK_PersonContacts_ContactTypes_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalTable: "ContactTypes",
                        principalColumn: "ContactTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonContacts_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonIdentityDocuments",
                columns: table => new
                {
                    PersonIdentityDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdentityDocumentTypeId = table.Column<int>(type: "integer", nullable: false),
                    EncryptedIdentifier = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    LookupHmac = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    LastFour = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    VerifiedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonIdentityDocuments", x => x.PersonIdentityDocumentId);
                    table.ForeignKey(
                        name: "FK_PersonIdentityDocuments_IdentityDocumentTypes_IdentityDocum~",
                        column: x => x.IdentityDocumentTypeId,
                        principalTable: "IdentityDocumentTypes",
                        principalColumn: "IdentityDocumentTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonIdentityDocuments_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchoolMemberships",
                columns: table => new
                {
                    SchoolMembershipId = table.Column<Guid>(type: "uuid", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleTypeId = table.Column<int>(type: "integer", nullable: false),
                    MemberNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolMemberships", x => x.SchoolMembershipId);
                    table.ForeignKey(
                        name: "FK_SchoolMemberships_RoleTypes_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalTable: "RoleTypes",
                        principalColumn: "RoleTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolMemberships_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolMemberships_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Guardians",
                columns: table => new
                {
                    GuardianId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentMembershipId = table.Column<Guid>(type: "uuid", nullable: false),
                    GuardianPersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    GuardianTypeId = table.Column<int>(type: "integer", nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    CanReceiveCommunications = table.Column<bool>(type: "boolean", nullable: false),
                    IsAuthorizedForPickup = table.Column<bool>(type: "boolean", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guardians", x => x.GuardianId);
                    table.ForeignKey(
                        name: "FK_Guardians_GuardianTypes_GuardianRel~",
                        column: x => x.GuardianTypeId,
                        principalTable: "GuardianTypes",
                        principalColumn: "GuardianTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guardians_People_GuardianPersonId",
                        column: x => x.GuardianPersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guardians_SchoolMemberships_StudentMembershipId",
                        column: x => x.StudentMembershipId,
                        principalTable: "SchoolMemberships",
                        principalColumn: "SchoolMembershipId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MemberDocuments",
                columns: table => new
                {
                    MemberDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    SchoolMembershipId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "integer", nullable: false),
                    StorageKey = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    ContentHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    UploadedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UploadedBySubject = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ExpiresAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    VerifiedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberDocuments", x => x.MemberDocumentId);
                    table.ForeignKey(
                        name: "FK_MemberDocuments_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "DocumentTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberDocuments_SchoolMemberships_SchoolMembershipId",
                        column: x => x.SchoolMembershipId,
                        principalTable: "SchoolMemberships",
                        principalColumn: "SchoolMembershipId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MemberMedicalProfiles",
                columns: table => new
                {
                    SchoolMembershipId = table.Column<Guid>(type: "uuid", nullable: false),
                    BloodGroupId = table.Column<int>(type: "integer", nullable: true),
                    MedicalNotes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberMedicalProfiles", x => x.SchoolMembershipId);
                    table.ForeignKey(
                        name: "FK_MemberMedicalProfiles_BloodGroups_BloodGroupId",
                        column: x => x.BloodGroupId,
                        principalTable: "BloodGroups",
                        principalColumn: "BloodGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberMedicalProfiles_SchoolMemberships_SchoolMembershipId",
                        column: x => x.SchoolMembershipId,
                        principalTable: "SchoolMemberships",
                        principalColumn: "SchoolMembershipId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentPreviousSchools",
                columns: table => new
                {
                    StudentPreviousSchoolId = table.Column<Guid>(type: "uuid", nullable: false),
                    SchoolMembershipId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CountryCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    LastGrade = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPreviousSchools", x => x.StudentPreviousSchoolId);
                    table.ForeignKey(
                        name: "FK_StudentPreviousSchools_SchoolMemberships_SchoolMembershipId",
                        column: x => x.SchoolMembershipId,
                        principalTable: "SchoolMemberships",
                        principalColumn: "SchoolMembershipId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressTypes_Code",
                table: "AddressTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BloodGroups_Code",
                table: "BloodGroups",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactTypes_Code",
                table: "ContactTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_Code",
                table: "DocumentTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guardians_GuardianPersonId",
                table: "Guardians",
                column: "GuardianPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Guardians_GuardianTypeId",
                table: "Guardians",
                column: "GuardianTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Guardians_StudentMembershipId_GuardianPersonId",
                table: "Guardians",
                columns: new[] { "StudentMembershipId", "GuardianPersonId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GuardianTypes_Code",
                table: "GuardianTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityDocumentTypes_Code",
                table: "IdentityDocumentTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberDocuments_DocumentTypeId",
                table: "MemberDocuments",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberDocuments_SchoolMembershipId",
                table: "MemberDocuments",
                column: "SchoolMembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberMedicalProfiles_BloodGroupId",
                table: "MemberMedicalProfiles",
                column: "BloodGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleTypes_Code",
                table: "RoleTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonAddresses_AddressTypeId",
                table: "PersonAddresses",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAddresses_PersonId",
                table: "PersonAddresses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonContacts_ContactTypeId",
                table: "PersonContacts",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonContacts_PersonId",
                table: "PersonContacts",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonIdentityDocuments_IdentityDocumentTypeId_LookupHmac",
                table: "PersonIdentityDocuments",
                columns: new[] { "IdentityDocumentTypeId", "LookupHmac" },
                unique: true,
                filter: "\"LookupHmac\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonIdentityDocuments_PersonId",
                table: "PersonIdentityDocuments",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolMemberships_RoleTypeId",
                table: "SchoolMemberships",
                column: "RoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolMemberships_PersonId",
                table: "SchoolMemberships",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolMemberships_SchoolId_MemberNumber",
                table: "SchoolMemberships",
                columns: new[] { "SchoolId", "MemberNumber" },
                unique: true,
                filter: "\"MemberNumber\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPreviousSchools_SchoolMembershipId",
                table: "StudentPreviousSchools",
                column: "SchoolMembershipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guardians");

            migrationBuilder.DropTable(
                name: "MemberDocuments");

            migrationBuilder.DropTable(
                name: "MemberMedicalProfiles");

            migrationBuilder.DropTable(
                name: "PersonAddresses");

            migrationBuilder.DropTable(
                name: "PersonContacts");

            migrationBuilder.DropTable(
                name: "PersonIdentityDocuments");

            migrationBuilder.DropTable(
                name: "StudentPreviousSchools");

            migrationBuilder.DropTable(
                name: "GuardianTypes");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "BloodGroups");

            migrationBuilder.DropTable(
                name: "AddressTypes");

            migrationBuilder.DropTable(
                name: "ContactTypes");

            migrationBuilder.DropTable(
                name: "IdentityDocumentTypes");

            migrationBuilder.DropTable(
                name: "SchoolMemberships");

            migrationBuilder.DropTable(
                name: "RoleTypes");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}

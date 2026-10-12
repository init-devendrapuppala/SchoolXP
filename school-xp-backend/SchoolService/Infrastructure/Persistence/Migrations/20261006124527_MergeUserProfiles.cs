using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MergeUserProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_guardians_people_guardian_person_id",
                schema: "sd",
                table: "guardians");

            migrationBuilder.DropForeignKey(
                name: "fk_person_addresses_people_person_id",
                schema: "per",
                table: "person_addresses");

            migrationBuilder.DropForeignKey(
                name: "fk_person_contacts_people_person_id",
                schema: "per",
                table: "person_contacts");

            migrationBuilder.DropForeignKey(
                name: "fk_person_identity_documents_people_person_id",
                schema: "per",
                table: "person_identity_documents");

            migrationBuilder.DropForeignKey(
                name: "fk_school_memberships_people_person_id",
                schema: "sd",
                table: "school_memberships");

            migrationBuilder.RenameColumn(
                name: "person_id",
                schema: "sd",
                table: "school_memberships",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_school_memberships_person_id",
                schema: "sd",
                table: "school_memberships",
                newName: "ix_school_memberships_user_id");

            migrationBuilder.RenameColumn(
                name: "person_id",
                schema: "per",
                table: "person_identity_documents",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_person_identity_documents_person_id",
                schema: "per",
                table: "person_identity_documents",
                newName: "ix_person_identity_documents_user_id");

            migrationBuilder.RenameColumn(
                name: "person_id",
                schema: "per",
                table: "person_contacts",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_person_contacts_person_id",
                schema: "per",
                table: "person_contacts",
                newName: "ix_person_contacts_user_id");

            migrationBuilder.RenameColumn(
                name: "person_id",
                schema: "per",
                table: "person_addresses",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_person_addresses_person_id",
                schema: "per",
                table: "person_addresses",
                newName: "ix_person_addresses_user_id");

            migrationBuilder.RenameColumn(
                name: "guardian_person_id",
                schema: "sd",
                table: "guardians",
                newName: "guardian_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_guardians_student_membership_id_guardian_person_id",
                schema: "sd",
                table: "guardians",
                newName: "ix_guardians_student_membership_id_guardian_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_guardians_guardian_person_id",
                schema: "sd",
                table: "guardians",
                newName: "ix_guardians_guardian_user_id");

            migrationBuilder.CreateTable(
                name: "user_profiles",
                schema: "per",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    middle_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    gender = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_platform_admin = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_profiles", x => x.user_id);
                });

            migrationBuilder.Sql(
                """
                INSERT INTO per.user_profiles (
                    user_id, first_name, middle_name, last_name, email, gender, date_of_birth,
                    is_active, is_platform_admin, created_at_utc, updated_at_utc)
                SELECT person_id, first_name, middle_name, last_name, email_address, gender, date_of_birth,
                       TRUE, FALSE, now(), NULL
                FROM per.people;
                """);

            migrationBuilder.CreateTable(
                name: "user_identities",
                schema: "per",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sub = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_identities", x => new { x.user_id, x.sub });
                    table.ForeignKey(
                        name: "fk_user_identities_user_profiles_user_id",
                        column: x => x.user_id,
                        principalSchema: "per",
                        principalTable: "user_profiles",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_identities_sub",
                schema: "per",
                table: "user_identities",
                column: "sub",
                unique: true);

            migrationBuilder.DropTable(
                name: "people",
                schema: "per");

            migrationBuilder.AddForeignKey(
                name: "fk_guardians_user_profiles_guardian_user_id",
                schema: "sd",
                table: "guardians",
                column: "guardian_user_id",
                principalSchema: "per",
                principalTable: "user_profiles",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_addresses_user_profiles_user_id",
                schema: "per",
                table: "person_addresses",
                column: "user_id",
                principalSchema: "per",
                principalTable: "user_profiles",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_contacts_user_profiles_user_id",
                schema: "per",
                table: "person_contacts",
                column: "user_id",
                principalSchema: "per",
                principalTable: "user_profiles",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_identity_documents_user_profiles_user_id",
                schema: "per",
                table: "person_identity_documents",
                column: "user_id",
                principalSchema: "per",
                principalTable: "user_profiles",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_school_memberships_user_profiles_user_id",
                schema: "sd",
                table: "school_memberships",
                column: "user_id",
                principalSchema: "per",
                principalTable: "user_profiles",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_guardians_user_profiles_guardian_user_id",
                schema: "sd",
                table: "guardians");

            migrationBuilder.DropForeignKey(
                name: "fk_person_addresses_user_profiles_user_id",
                schema: "per",
                table: "person_addresses");

            migrationBuilder.DropForeignKey(
                name: "fk_person_contacts_user_profiles_user_id",
                schema: "per",
                table: "person_contacts");

            migrationBuilder.DropForeignKey(
                name: "fk_person_identity_documents_user_profiles_user_id",
                schema: "per",
                table: "person_identity_documents");

            migrationBuilder.DropForeignKey(
                name: "fk_school_memberships_user_profiles_user_id",
                schema: "sd",
                table: "school_memberships");

            migrationBuilder.DropTable(
                name: "user_identities",
                schema: "per");

            migrationBuilder.CreateTable(
                name: "people",
                schema: "per",
                columns: table => new
                {
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    email_address = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    gender = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    identity_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    middle_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_people", x => x.person_id);
                });

            migrationBuilder.Sql(
                """
                INSERT INTO per.people (
                    person_id, date_of_birth, email_address, first_name, gender, identity_user_id, last_name, middle_name)
                SELECT user_id, date_of_birth, email, first_name, gender, NULL, last_name, middle_name
                FROM per.user_profiles;
                """);

            migrationBuilder.DropTable(
                name: "user_profiles",
                schema: "per");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "sd",
                table: "school_memberships",
                newName: "person_id");

            migrationBuilder.RenameIndex(
                name: "ix_school_memberships_user_id",
                schema: "sd",
                table: "school_memberships",
                newName: "ix_school_memberships_person_id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "per",
                table: "person_identity_documents",
                newName: "person_id");

            migrationBuilder.RenameIndex(
                name: "ix_person_identity_documents_user_id",
                schema: "per",
                table: "person_identity_documents",
                newName: "ix_person_identity_documents_person_id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "per",
                table: "person_contacts",
                newName: "person_id");

            migrationBuilder.RenameIndex(
                name: "ix_person_contacts_user_id",
                schema: "per",
                table: "person_contacts",
                newName: "ix_person_contacts_person_id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "per",
                table: "person_addresses",
                newName: "person_id");

            migrationBuilder.RenameIndex(
                name: "ix_person_addresses_user_id",
                schema: "per",
                table: "person_addresses",
                newName: "ix_person_addresses_person_id");

            migrationBuilder.RenameColumn(
                name: "guardian_user_id",
                schema: "sd",
                table: "guardians",
                newName: "guardian_person_id");

            migrationBuilder.RenameIndex(
                name: "ix_guardians_student_membership_id_guardian_user_id",
                schema: "sd",
                table: "guardians",
                newName: "ix_guardians_student_membership_id_guardian_person_id");

            migrationBuilder.RenameIndex(
                name: "ix_guardians_guardian_user_id",
                schema: "sd",
                table: "guardians",
                newName: "ix_guardians_guardian_person_id");

            migrationBuilder.CreateIndex(
                name: "ix_people_identity_user_id",
                schema: "per",
                table: "people",
                column: "identity_user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_guardians_people_guardian_person_id",
                schema: "sd",
                table: "guardians",
                column: "guardian_person_id",
                principalSchema: "per",
                principalTable: "people",
                principalColumn: "person_id",
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
                name: "fk_person_contacts_people_person_id",
                schema: "per",
                table: "person_contacts",
                column: "person_id",
                principalSchema: "per",
                principalTable: "people",
                principalColumn: "person_id",
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
                name: "fk_school_memberships_people_person_id",
                schema: "sd",
                table: "school_memberships",
                column: "person_id",
                principalSchema: "per",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolService.Infrastructure.Persistence;

#nullable disable

namespace SchoolService.Infrastructure.Persistence.Migrations;

[DbContext(typeof(SchoolDbContext))]
[Migration("20261002120000_ReplaceMembershipIsActiveWithStatus")]
public partial class ReplaceMembershipIsActiveWithStatus : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "membership_statuses",
            schema: "sm",
            columns: table => new
            {
                membership_status_id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_membership_statuses", x => x.membership_status_id);
            });

        migrationBuilder.CreateIndex(
            name: "ix_membership_statuses_code",
            schema: "sm",
            table: "membership_statuses",
            column: "code",
            unique: true);

        migrationBuilder.Sql(
            """
            INSERT INTO sm.membership_statuses (membership_status_id, code, description)
            VALUES
                (1, 'draft', 'Draft'),
                (2, 'pending', 'Pending'),
                (3, 'active', 'Active'),
                (4, 'inactive', 'Inactive');

            SELECT setval(
                pg_get_serial_sequence('sm.membership_statuses', 'membership_status_id'),
                (SELECT MAX(membership_status_id) FROM sm.membership_statuses),
                true);
            """);

        migrationBuilder.AddColumn<int>(
            name: "membership_status_id",
            schema: "sd",
            table: "school_memberships",
            type: "integer",
            nullable: true);

        migrationBuilder.Sql(
            """
            UPDATE sd.school_memberships
            SET membership_status_id = CASE
                WHEN is_active THEN 3
                ELSE 4
            END;
            """);

        migrationBuilder.AlterColumn<int>(
            name: "membership_status_id",
            schema: "sd",
            table: "school_memberships",
            type: "integer",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "integer",
            oldNullable: true);

        migrationBuilder.CreateIndex(
            name: "ix_school_memberships_membership_status_id",
            schema: "sd",
            table: "school_memberships",
            column: "membership_status_id");

        migrationBuilder.AddForeignKey(
            name: "fk_school_memberships_membership_statuses_membership_status_id",
            schema: "sd",
            table: "school_memberships",
            column: "membership_status_id",
            principalSchema: "sm",
            principalTable: "membership_statuses",
            principalColumn: "membership_status_id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.DropColumn(
            name: "is_active",
            schema: "sd",
            table: "school_memberships");

        migrationBuilder.DropColumn(
            name: "start_date",
            schema: "sd",
            table: "school_memberships");

        migrationBuilder.DropColumn(
            name: "end_date",
            schema: "sd",
            table: "school_memberships");

        migrationBuilder.DropIndex(
            name: "ix_person_identity_documents_identity_document_type_id_lookup_",
            schema: "per",
            table: "person_identity_documents");

        migrationBuilder.DropColumn(
            name: "last_four",
            schema: "per",
            table: "person_identity_documents");

        migrationBuilder.DropColumn(
            name: "lookup_hmac",
            schema: "per",
            table: "person_identity_documents");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "is_active",
            schema: "sd",
            table: "school_memberships",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.Sql(
            """
            UPDATE sd.school_memberships AS membership
            SET is_active = status.code = 'active'
            FROM sm.membership_statuses AS status
            WHERE membership.membership_status_id = status.membership_status_id;
            """);

        migrationBuilder.AddColumn<DateOnly>(
            name: "start_date",
            schema: "sd",
            table: "school_memberships",
            type: "date",
            nullable: false,
            defaultValueSql: "CURRENT_DATE");

        migrationBuilder.AddColumn<DateOnly>(
            name: "end_date",
            schema: "sd",
            table: "school_memberships",
            type: "date",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "last_four",
            schema: "per",
            table: "person_identity_documents",
            type: "character varying(4)",
            maxLength: 4,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "lookup_hmac",
            schema: "per",
            table: "person_identity_documents",
            type: "character varying(128)",
            maxLength: 128,
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "ix_person_identity_documents_identity_document_type_id_lookup_",
            schema: "per",
            table: "person_identity_documents",
            columns: new[] { "identity_document_type_id", "lookup_hmac" },
            unique: true,
            filter: "\"lookup_hmac\" IS NOT NULL");

        migrationBuilder.DropForeignKey(
            name: "fk_school_memberships_membership_statuses_membership_status_id",
            schema: "sd",
            table: "school_memberships");

        migrationBuilder.DropIndex(
            name: "ix_school_memberships_membership_status_id",
            schema: "sd",
            table: "school_memberships");

        migrationBuilder.DropColumn(
            name: "membership_status_id",
            schema: "sd",
            table: "school_memberships");

        migrationBuilder.DropTable(
            name: "membership_statuses",
            schema: "sm");
    }
}
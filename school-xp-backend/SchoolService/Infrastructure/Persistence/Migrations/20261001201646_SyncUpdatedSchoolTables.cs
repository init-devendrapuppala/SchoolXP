using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SyncUpdatedSchoolTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_school_memberships_member_types_role_type_id",
                schema: "sd",
                table: "school_memberships");

            migrationBuilder.DropForeignKey(
                name: "fk_guardians_guardian_types_guardian_type_id",
                schema: "sd",
                table: "guardians");

            migrationBuilder.DropPrimaryKey(
                name: "pk_guardian_relationship_types",
                schema: "sm",
                table: "guardian_relationship_types");

            migrationBuilder.RenameTable(
                name: "guardian_relationship_types",
                schema: "sm",
                newName: "guardian_types",
                newSchema: "sm");

            migrationBuilder.RenameColumn(
                name: "member_type_id",
                schema: "sm",
                table: "member_types",
                newName: "role_type_id");

            migrationBuilder.RenameIndex(
                name: "ix_guardian_relationship_types_code",
                schema: "sm",
                table: "guardian_types",
                newName: "ix_guardian_types_code");

            migrationBuilder.AddPrimaryKey(
                name: "pk_guardian_types",
                schema: "sm",
                table: "guardian_types",
                column: "guardian_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_guardians_guardian_types_guardian_type_id",
                schema: "sd",
                table: "guardians",
                column: "guardian_type_id",
                principalSchema: "sm",
                principalTable: "guardian_types",
                principalColumn: "guardian_type_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_school_memberships_role_types_role_type_id",
                schema: "sd",
                table: "school_memberships",
                column: "role_type_id",
                principalSchema: "sm",
                principalTable: "member_types",
                principalColumn: "role_type_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_school_memberships_role_types_role_type_id",
                schema: "sd",
                table: "school_memberships");

            migrationBuilder.DropForeignKey(
                name: "fk_guardians_guardian_types_guardian_type_id",
                schema: "sd",
                table: "guardians");

            migrationBuilder.DropPrimaryKey(
                name: "pk_guardian_types",
                schema: "sm",
                table: "guardian_types");

            migrationBuilder.RenameTable(
                name: "guardian_types",
                schema: "sm",
                newName: "guardian_relationship_types",
                newSchema: "sm");

            migrationBuilder.RenameColumn(
                name: "role_type_id",
                schema: "sm",
                table: "member_types",
                newName: "member_type_id");

            migrationBuilder.RenameIndex(
                name: "ix_guardian_types_code",
                schema: "sm",
                table: "guardian_relationship_types",
                newName: "ix_guardian_relationship_types_code");

            migrationBuilder.AddPrimaryKey(
                name: "pk_guardian_relationship_types",
                schema: "sm",
                table: "guardian_relationship_types",
                column: "guardian_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_guardians_guardian_types_guardian_type_id",
                schema: "sd",
                table: "guardians",
                column: "guardian_type_id",
                principalSchema: "sm",
                principalTable: "guardian_relationship_types",
                principalColumn: "guardian_type_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_school_memberships_member_types_role_type_id",
                schema: "sd",
                table: "school_memberships",
                column: "role_type_id",
                principalSchema: "sm",
                principalTable: "member_types",
                principalColumn: "member_type_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SyncUpdatedSchoolEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "fk_school_memberships_member_types_member_type_id",
                schema: "sd",
                table: "school_memberships");

            migrationBuilder.DropPrimaryKey(
                name: "pk_guardian_relationships",
                schema: "sd",
                table: "guardian_relationships");

            migrationBuilder.DropColumn(
                name: "end_date",
                schema: "sd",
                table: "guardian_relationships");

            migrationBuilder.RenameTable(
                name: "guardian_relationships",
                schema: "sd",
                newName: "guardians",
                newSchema: "sd");

            migrationBuilder.RenameColumn(
                name: "updated_by_subject",
                schema: "sd",
                table: "school_memberships",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "created_by_subject",
                schema: "sd",
                table: "school_memberships",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "member_type_id",
                schema: "sd",
                table: "school_memberships",
                newName: "role_type_id");

            migrationBuilder.RenameIndex(
                name: "ix_school_memberships_member_type_id",
                schema: "sd",
                table: "school_memberships",
                newName: "ix_school_memberships_role_type_id");

            migrationBuilder.RenameColumn(
                name: "guardian_relationship_type_id",
                schema: "sm",
                table: "guardian_relationship_types",
                newName: "guardian_type_id");

            migrationBuilder.RenameColumn(
                name: "guardian_relationship_type_id",
                schema: "sd",
                table: "guardians",
                newName: "guardian_type_id");

            migrationBuilder.RenameColumn(
                name: "guardian_relationship_id",
                schema: "sd",
                table: "guardians",
                newName: "guardian_id");

            migrationBuilder.RenameIndex(
                name: "ix_guardian_relationships_student_membership_id_guardian_perso",
                schema: "sd",
                table: "guardians",
                newName: "ix_guardians_student_membership_id_guardian_person_id");

            migrationBuilder.RenameIndex(
                name: "ix_guardian_relationships_guardian_relationship_type_id",
                schema: "sd",
                table: "guardians",
                newName: "ix_guardians_guardian_type_id");

            migrationBuilder.RenameIndex(
                name: "ix_guardian_relationships_guardian_person_id",
                schema: "sd",
                table: "guardians",
                newName: "ix_guardians_guardian_person_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_guardians",
                schema: "sd",
                table: "guardians",
                column: "guardian_id");

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
                name: "fk_guardians_people_guardian_person_id",
                schema: "sd",
                table: "guardians",
                column: "guardian_person_id",
                principalSchema: "per",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_guardians_school_memberships_student_membership_id",
                schema: "sd",
                table: "guardians",
                column: "student_membership_id",
                principalSchema: "sd",
                principalTable: "school_memberships",
                principalColumn: "school_membership_id",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_guardians_guardian_types_guardian_type_id",
                schema: "sd",
                table: "guardians");

            migrationBuilder.DropForeignKey(
                name: "fk_guardians_people_guardian_person_id",
                schema: "sd",
                table: "guardians");

            migrationBuilder.DropForeignKey(
                name: "fk_guardians_school_memberships_student_membership_id",
                schema: "sd",
                table: "guardians");

            migrationBuilder.DropForeignKey(
                name: "fk_school_memberships_member_types_role_type_id",
                schema: "sd",
                table: "school_memberships");

            migrationBuilder.DropPrimaryKey(
                name: "pk_guardians",
                schema: "sd",
                table: "guardians");

            migrationBuilder.RenameTable(
                name: "guardians",
                schema: "sd",
                newName: "guardian_relationships",
                newSchema: "sd");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                schema: "sd",
                table: "school_memberships",
                newName: "updated_by_subject");

            migrationBuilder.RenameColumn(
                name: "created_by",
                schema: "sd",
                table: "school_memberships",
                newName: "created_by_subject");

            migrationBuilder.RenameColumn(
                name: "role_type_id",
                schema: "sd",
                table: "school_memberships",
                newName: "member_type_id");

            migrationBuilder.RenameIndex(
                name: "ix_school_memberships_role_type_id",
                schema: "sd",
                table: "school_memberships",
                newName: "ix_school_memberships_member_type_id");

            migrationBuilder.RenameColumn(
                name: "guardian_type_id",
                schema: "sm",
                table: "guardian_relationship_types",
                newName: "guardian_relationship_type_id");

            migrationBuilder.RenameColumn(
                name: "guardian_type_id",
                schema: "sd",
                table: "guardian_relationships",
                newName: "guardian_relationship_type_id");

            migrationBuilder.RenameColumn(
                name: "guardian_id",
                schema: "sd",
                table: "guardian_relationships",
                newName: "guardian_relationship_id");

            migrationBuilder.RenameIndex(
                name: "ix_guardians_student_membership_id_guardian_person_id",
                schema: "sd",
                table: "guardian_relationships",
                newName: "ix_guardian_relationships_student_membership_id_guardian_perso");

            migrationBuilder.RenameIndex(
                name: "ix_guardians_guardian_type_id",
                schema: "sd",
                table: "guardian_relationships",
                newName: "ix_guardian_relationships_guardian_relationship_type_id");

            migrationBuilder.RenameIndex(
                name: "ix_guardians_guardian_person_id",
                schema: "sd",
                table: "guardian_relationships",
                newName: "ix_guardian_relationships_guardian_person_id");

            migrationBuilder.AddColumn<DateOnly>(
                name: "end_date",
                schema: "sd",
                table: "guardian_relationships",
                type: "date",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_guardian_relationships",
                schema: "sd",
                table: "guardian_relationships",
                column: "guardian_relationship_id");

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
                name: "fk_school_memberships_member_types_member_type_id",
                schema: "sd",
                table: "school_memberships",
                column: "member_type_id",
                principalSchema: "sm",
                principalTable: "member_types",
                principalColumn: "member_type_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

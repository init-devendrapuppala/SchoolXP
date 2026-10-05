using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using SchoolService.Infrastructure.Persistence;

#nullable disable

namespace SchoolService.Infrastructure.Persistence.Migrations;

[DbContext(typeof(SchoolDbContext))]
[Migration("20261004100000_AddStaffEmergencyContacts")]
public partial class AddStaffEmergencyContacts : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "staff_emergency_contacts",
            schema: "sd",
            columns: table => new
            {
                staff_emergency_contact_id = table.Column<Guid>(type: "uuid", nullable: false),
                school_membership_id = table.Column<Guid>(type: "uuid", nullable: false),
                first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                relationship = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                phone_number = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                email_address = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_staff_emergency_contacts", x => x.staff_emergency_contact_id);
                table.ForeignKey(
                    name: "fk_staff_emergency_contacts_membership",
                    column: x => x.school_membership_id,
                    principalSchema: "sd",
                    principalTable: "school_memberships",
                    principalColumn: "school_membership_id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "ix_staff_emergency_contacts_school_membership_id",
            schema: "sd",
            table: "staff_emergency_contacts",
            column: "school_membership_id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "staff_emergency_contacts",
            schema: "sd");
    }
}
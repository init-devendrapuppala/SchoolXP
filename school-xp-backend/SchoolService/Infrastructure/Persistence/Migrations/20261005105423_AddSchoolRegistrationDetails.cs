using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSchoolRegistrationDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address_line",
                schema: "sm",
                table: "schools",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "affiliation",
                schema: "sm",
                table: "schools",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                schema: "sm",
                table: "schools",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "country",
                schema: "sm",
                table: "schools",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email_address",
                schema: "sm",
                table: "schools",
                type: "character varying(320)",
                maxLength: 320,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "established_year",
                schema: "sm",
                table: "schools",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                schema: "sm",
                table: "schools",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "postal_code",
                schema: "sm",
                table: "schools",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "state_or_province",
                schema: "sm",
                table: "schools",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "website",
                schema: "sm",
                table: "schools",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "date_of_birth",
                schema: "per",
                table: "people",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                schema: "per",
                table: "people",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address_line",
                schema: "sm",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "affiliation",
                schema: "sm",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "city",
                schema: "sm",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "country",
                schema: "sm",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "email_address",
                schema: "sm",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "established_year",
                schema: "sm",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "phone_number",
                schema: "sm",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "postal_code",
                schema: "sm",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "state_or_province",
                schema: "sm",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "website",
                schema: "sm",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "date_of_birth",
                schema: "per",
                table: "people");

            migrationBuilder.DropColumn(
                name: "gender",
                schema: "per",
                table: "people");
        }
    }
}

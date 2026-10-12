using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileAccountFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_account",
                schema: "per",
                table: "user_profiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "ix_user_profiles_email",
                schema: "per",
                table: "user_profiles",
                column: "email",
                unique: true,
                filter: "\"is_account\" = TRUE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_profiles_email",
                schema: "per",
                table: "user_profiles");

            migrationBuilder.DropColumn(
                name: "is_account",
                schema: "per",
                table: "user_profiles");
        }
    }
}

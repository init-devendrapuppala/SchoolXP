using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityUserIdToPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdentityUserId",
                schema: "per",
                table: "People",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_IdentityUserId",
                schema: "per",
                table: "People",
                column: "IdentityUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_IdentityUserId",
                schema: "per",
                table: "People");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                schema: "per",
                table: "People");
        }
    }
}

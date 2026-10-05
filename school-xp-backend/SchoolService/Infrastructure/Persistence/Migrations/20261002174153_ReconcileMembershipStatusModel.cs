using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReconcileMembershipStatusModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_person_identity_documents_identity_document_type_id",
                schema: "per",
                table: "person_identity_documents",
                column: "identity_document_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_person_identity_documents_identity_document_type_id",
                schema: "per",
                table: "person_identity_documents");
        }
    }
}

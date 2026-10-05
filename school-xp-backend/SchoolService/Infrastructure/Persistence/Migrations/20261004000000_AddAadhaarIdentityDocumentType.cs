using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using SchoolService.Infrastructure.Persistence;

#nullable disable

namespace SchoolService.Infrastructure.Persistence.Migrations;

[DbContext(typeof(SchoolDbContext))]
[Migration("20261004000000_AddAadhaarIdentityDocumentType")]
public partial class AddAadhaarIdentityDocumentType : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
            INSERT INTO sm.identity_document_types (code, description)
            VALUES ('aadhaar', 'Aadhaar')
            ON CONFLICT (code) DO NOTHING;
            """);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
            DELETE FROM sm.identity_document_types AS document_type
            WHERE document_type.code = 'aadhaar'
              AND NOT EXISTS (
                  SELECT 1
                  FROM per.person_identity_documents AS person_document
                  WHERE person_document.identity_document_type_id = document_type.identity_document_type_id);
            """);
    }
}
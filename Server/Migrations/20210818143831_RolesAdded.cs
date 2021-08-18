using Microsoft.EntityFrameworkCore.Migrations;

namespace Horrografia.Server.Migrations
{
    public partial class RolesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e86548c8-8f5f-4f03-b91b-5427443502be", "ea004ed4-aebf-427f-85c4-f19bbb03a22a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a275508e-5126-4ed7-8105-e7bfd718a686", "72b4946a-9cae-4665-945f-1af22b9fe9dd", "Profesor", "PROFESOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a275508e-5126-4ed7-8105-e7bfd718a686");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e86548c8-8f5f-4f03-b91b-5427443502be");
        }
    }
}

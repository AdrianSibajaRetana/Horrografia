using Microsoft.EntityFrameworkCore.Migrations;

namespace Horrografia.Server.Migrations
{
    public partial class SchoolCodeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a275508e-5126-4ed7-8105-e7bfd718a686");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e86548c8-8f5f-4f03-b91b-5427443502be");

            migrationBuilder.AddColumn<string>(
                name: "CodigoEscuela",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "896b75c1-ab2c-4401-bdbe-1e5b42dde3ef", "35b10d1f-ea29-4970-a2e6-06de2ccb6cb0", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3b71b557-c787-4869-82b9-853ea5f48c1c", "02f4e3e7-c60d-40da-9302-dd2a279977e2", "Profesor", "PROFESOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b71b557-c787-4869-82b9-853ea5f48c1c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "896b75c1-ab2c-4401-bdbe-1e5b42dde3ef");

            migrationBuilder.DropColumn(
                name: "CodigoEscuela",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e86548c8-8f5f-4f03-b91b-5427443502be", "ea004ed4-aebf-427f-85c4-f19bbb03a22a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a275508e-5126-4ed7-8105-e7bfd718a686", "72b4946a-9cae-4665-945f-1af22b9fe9dd", "Profesor", "PROFESOR" });
        }
    }
}

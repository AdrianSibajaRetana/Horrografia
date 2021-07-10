using Microsoft.EntityFrameworkCore.Migrations;

namespace Horrografia.Server.Migrations
{
    public partial class NombreDeUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreDeUsuario",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreDeUsuario",
                table: "AspNetUsers");
        }
    }
}

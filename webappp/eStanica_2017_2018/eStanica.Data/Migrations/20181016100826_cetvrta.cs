using Microsoft.EntityFrameworkCore.Migrations;

namespace eStanica.Data.Migrations
{
    public partial class cetvrta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isValid",
                table: "Korisnici",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isValid",
                table: "Korisnici");
        }
    }
}

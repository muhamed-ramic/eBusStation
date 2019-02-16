using Microsoft.EntityFrameworkCore.Migrations;

namespace eStanica.Data.Migrations
{
    public partial class dodatoZanimanje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Zanimanje",
                table: "Korisnici",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zanimanje",
                table: "Korisnici");
        }
    }
}

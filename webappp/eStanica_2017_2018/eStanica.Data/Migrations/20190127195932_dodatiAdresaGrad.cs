using Microsoft.EntityFrameworkCore.Migrations;

namespace eStanica.Data.Migrations
{
    public partial class dodatiAdresaGrad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Korisnici",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grad",
                table: "Korisnici",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "Drzave",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "Grad",
                table: "Korisnici");

            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "Drzave",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

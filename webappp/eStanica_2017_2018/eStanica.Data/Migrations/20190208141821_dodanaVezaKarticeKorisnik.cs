using Microsoft.EntityFrameworkCore.Migrations;

namespace eStanica.Data.Migrations
{
    public partial class dodanaVezaKarticeKorisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KlijentiId",
                table: "Kartice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Kartice_KlijentiId",
                table: "Kartice",
                column: "KlijentiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kartice_Korisnici_KlijentiId",
                table: "Kartice",
                column: "KlijentiId",
                principalTable: "Korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kartice_Korisnici_KlijentiId",
                table: "Kartice");

            migrationBuilder.DropIndex(
                name: "IX_Kartice_KlijentiId",
                table: "Kartice");

            migrationBuilder.DropColumn(
                name: "KlijentiId",
                table: "Kartice");
        }
    }
}

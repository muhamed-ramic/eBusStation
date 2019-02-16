using Microsoft.EntityFrameworkCore.Migrations;

namespace eStanica.Data.Migrations
{
    public partial class izmjenaUTransakcijiTabelaPogresnoNapisanKljuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transakcije_Korisnici_KlijentiId",
                table: "Transakcije");

            migrationBuilder.DropColumn(
                name: "KlijentId",
                table: "Transakcije");

            migrationBuilder.AlterColumn<int>(
                name: "KlijentiId",
                table: "Transakcije",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcije_Korisnici_KlijentiId",
                table: "Transakcije",
                column: "KlijentiId",
                principalTable: "Korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transakcije_Korisnici_KlijentiId",
                table: "Transakcije");

            migrationBuilder.AlterColumn<int>(
                name: "KlijentiId",
                table: "Transakcije",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "KlijentId",
                table: "Transakcije",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcije_Korisnici_KlijentiId",
                table: "Transakcije",
                column: "KlijentiId",
                principalTable: "Korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

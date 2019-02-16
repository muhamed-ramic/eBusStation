using Microsoft.EntityFrameworkCore.Migrations;

namespace eStanica.Data.Migrations
{
    public partial class popravljenaVezaKartePosjecujeLokacije : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PosjecujeLokacije_Karte_KartaId",
                table: "PosjecujeLokacije");

            migrationBuilder.DropIndex(
                name: "IX_PosjecujeLokacije_KartaId",
                table: "PosjecujeLokacije");

            migrationBuilder.DropColumn(
                name: "KartaId",
                table: "PosjecujeLokacije");

            migrationBuilder.AddColumn<int>(
                name: "PosjecujeLokacijeId",
                table: "Karte",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Karte_PosjecujeLokacijeId",
                table: "Karte",
                column: "PosjecujeLokacijeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Karte_PosjecujeLokacije_PosjecujeLokacijeId",
                table: "Karte",
                column: "PosjecujeLokacijeId",
                principalTable: "PosjecujeLokacije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Karte_PosjecujeLokacije_PosjecujeLokacijeId",
                table: "Karte");

            migrationBuilder.DropIndex(
                name: "IX_Karte_PosjecujeLokacijeId",
                table: "Karte");

            migrationBuilder.DropColumn(
                name: "PosjecujeLokacijeId",
                table: "Karte");

            migrationBuilder.AddColumn<int>(
                name: "KartaId",
                table: "PosjecujeLokacije",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PosjecujeLokacije_KartaId",
                table: "PosjecujeLokacije",
                column: "KartaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PosjecujeLokacije_Karte_KartaId",
                table: "PosjecujeLokacije",
                column: "KartaId",
                principalTable: "Karte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

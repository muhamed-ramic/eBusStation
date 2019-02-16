using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eStanica.Data.Migrations
{
    public partial class korisnikUloga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisnikUloge");

            migrationBuilder.AddColumn<int>(
                name: "UlogeId",
                table: "Korisnici",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_UlogeId",
                table: "Korisnici",
                column: "UlogeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnici_Uloge_UlogeId",
                table: "Korisnici",
                column: "UlogeId",
                principalTable: "Uloge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnici_Uloge_UlogeId",
                table: "Korisnici");

            migrationBuilder.DropIndex(
                name: "IX_Korisnici_UlogeId",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "UlogeId",
                table: "Korisnici");

            migrationBuilder.CreateTable(
                name: "KorisnikUloge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KorisnikId = table.Column<int>(nullable: false),
                    UlogeId = table.Column<int>(nullable: false),
                    datumLogiranja = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikUloge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorisnikUloge_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KorisnikUloge_Uloge_UlogeId",
                        column: x => x.UlogeId,
                        principalTable: "Uloge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUloge_KorisnikId",
                table: "KorisnikUloge",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUloge_UlogeId",
                table: "KorisnikUloge",
                column: "UlogeId");
        }
    }
}

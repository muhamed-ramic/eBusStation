using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eStanica.Data.Migrations
{
    public partial class dodanpopustLinije : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PopustNaLiniji",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PopustId = table.Column<int>(nullable: false),
                    LinijeId = table.Column<int>(nullable: false),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopustNaLiniji", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PopustNaLiniji_Linije_LinijeId",
                        column: x => x.LinijeId,
                        principalTable: "Linije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PopustNaLiniji_Popusti_PopustId",
                        column: x => x.PopustId,
                        principalTable: "Popusti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PopustNaLiniji_LinijeId",
                table: "PopustNaLiniji",
                column: "LinijeId");

            migrationBuilder.CreateIndex(
                name: "IX_PopustNaLiniji_PopustId",
                table: "PopustNaLiniji",
                column: "PopustId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopustNaLiniji");
        }
    }
}

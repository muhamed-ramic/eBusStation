using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eStanica.Data.Migrations
{
    public partial class dodanavrstaAutobusa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "vrstaBusa",
                table: "Autobusi");

            migrationBuilder.AddColumn<int>(
                name: "VrstaAutobusaId",
                table: "Autobusi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VrstaAutobusa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaAutobusa", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autobusi_VrstaAutobusaId",
                table: "Autobusi",
                column: "VrstaAutobusaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Autobusi_VrstaAutobusa_VrstaAutobusaId",
                table: "Autobusi",
                column: "VrstaAutobusaId",
                principalTable: "VrstaAutobusa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autobusi_VrstaAutobusa_VrstaAutobusaId",
                table: "Autobusi");

            migrationBuilder.DropTable(
                name: "VrstaAutobusa");

            migrationBuilder.DropIndex(
                name: "IX_Autobusi_VrstaAutobusaId",
                table: "Autobusi");

            migrationBuilder.DropColumn(
                name: "VrstaAutobusaId",
                table: "Autobusi");

            migrationBuilder.AddColumn<string>(
                name: "vrstaBusa",
                table: "Autobusi",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace eStanica.Data.Migrations
{
    public partial class dodanAtuobusIdNaLiniji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AutobusId",
                table: "Linije",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Linije_AutobusId",
                table: "Linije",
                column: "AutobusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Linije_Autobusi_AutobusId",
                table: "Linije",
                column: "AutobusId",
                principalTable: "Autobusi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Linije_Autobusi_AutobusId",
                table: "Linije");

            migrationBuilder.DropIndex(
                name: "IX_Linije_AutobusId",
                table: "Linije");

            migrationBuilder.DropColumn(
                name: "AutobusId",
                table: "Linije");
        }
    }
}

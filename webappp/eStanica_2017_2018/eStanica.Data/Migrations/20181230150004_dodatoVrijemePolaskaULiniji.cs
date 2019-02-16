using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eStanica.Data.Migrations
{
    public partial class dodatoVrijemePolaskaULiniji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "vrijemeDolaska",
                table: "PosjecujeLokacije",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "vrijemePolaska",
                table: "Linije",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "vrijemePolaska",
                table: "Linije");

            migrationBuilder.AlterColumn<DateTime>(
                name: "vrijemeDolaska",
                table: "PosjecujeLokacije",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

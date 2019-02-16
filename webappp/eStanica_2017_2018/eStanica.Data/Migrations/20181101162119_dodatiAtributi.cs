using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eStanica.Data.Migrations
{
    public partial class dodatiAtributi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KontaktTelefon",
                table: "Prevoznici",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "vrijemeDolaska",
                table: "PosjecujeLokacije",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "vrijemePolaska",
                table: "KretanjeAutobusa",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "vrijemeDolaska",
                table: "PosjecujeLokacije");

            migrationBuilder.DropColumn(
                name: "vrijemePolaska",
                table: "KretanjeAutobusa");

            migrationBuilder.AlterColumn<int>(
                name: "KontaktTelefon",
                table: "Prevoznici",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

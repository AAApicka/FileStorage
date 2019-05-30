using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Elinkx.FileStorage.DataLayer.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Content",
                table: "FileContent",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Content",
                table: "FileContent",
                nullable: true,
                oldClrType: typeof(byte[]));
        }
    }
}

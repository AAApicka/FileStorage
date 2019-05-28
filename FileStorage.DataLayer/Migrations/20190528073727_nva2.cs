using Microsoft.EntityFrameworkCore.Migrations;

namespace Elinkx.FileStorage.DataLayer.Migrations
{
    public partial class nva2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileVersion_Metadata_FileId",
                table: "FileVersion");

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "FileVersion",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_FileVersion_Metadata_FileId",
                table: "FileVersion",
                column: "FileId",
                principalTable: "Metadata",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileVersion_Metadata_FileId",
                table: "FileVersion");

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "FileVersion",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileVersion_Metadata_FileId",
                table: "FileVersion",
                column: "FileId",
                principalTable: "Metadata",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

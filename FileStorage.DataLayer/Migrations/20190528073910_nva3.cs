using Microsoft.EntityFrameworkCore.Migrations;

namespace Elinkx.FileStorage.DataLayer.Migrations
{
    public partial class nva3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileVersion_FileContent_RowId",
                table: "FileVersion");

            migrationBuilder.DropIndex(
                name: "IX_FileVersion_RowId",
                table: "FileVersion");

            migrationBuilder.AlterColumn<int>(
                name: "RowId",
                table: "FileVersion",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_FileVersion_RowId",
                table: "FileVersion",
                column: "RowId",
                unique: true,
                filter: "[RowId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FileVersion_FileContent_RowId",
                table: "FileVersion",
                column: "RowId",
                principalTable: "FileContent",
                principalColumn: "RowId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileVersion_FileContent_RowId",
                table: "FileVersion");

            migrationBuilder.DropIndex(
                name: "IX_FileVersion_RowId",
                table: "FileVersion");

            migrationBuilder.AlterColumn<int>(
                name: "RowId",
                table: "FileVersion",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileVersion_RowId",
                table: "FileVersion",
                column: "RowId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileVersion_FileContent_RowId",
                table: "FileVersion",
                column: "RowId",
                principalTable: "FileContent",
                principalColumn: "RowId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

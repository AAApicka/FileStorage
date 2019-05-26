using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Elinkx.FileStorage.DataLayer.Migrations
{
    public partial class numeroUno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileContent",
                columns: table => new
                {
                    RowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileContent", x => x.RowId);
                });

            migrationBuilder.CreateTable(
                name: "Metadata",
                columns: table => new
                {
                    FileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentType = table.Column<string>(nullable: true),
                    SubjectId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DocumentId = table.Column<int>(nullable: false),
                    TypeId = table.Column<string>(nullable: true),
                    SubtypeId = table.Column<string>(nullable: true),
                    Signed = table.Column<bool>(nullable: false),
                    Reject = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    Changed = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadata", x => x.FileId);
                });

            migrationBuilder.CreateTable(
                name: "FileVersion",
                columns: table => new
                {
                    VersionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Changed = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    Size = table.Column<int>(nullable: false),
                    FileId = table.Column<int>(nullable: false),
                    RowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileVersion", x => x.VersionId);
                    table.ForeignKey(
                        name: "FK_FileVersion_Metadata_FileId",
                        column: x => x.FileId,
                        principalTable: "Metadata",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileVersion_FileContent_RowId",
                        column: x => x.RowId,
                        principalTable: "FileContent",
                        principalColumn: "RowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileVersion_FileId",
                table: "FileVersion",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileVersion_RowId",
                table: "FileVersion",
                column: "RowId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileVersion");

            migrationBuilder.DropTable(
                name: "Metadata");

            migrationBuilder.DropTable(
                name: "FileContent");
        }
    }
}

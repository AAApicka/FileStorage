using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Elinkx.FileStorage.DataLayer.Migrations
{
    public partial class mig1 : Migration
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
                    ContentType = table.Column<string>(maxLength: 150, nullable: false),
                    SubjectId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DocumentId = table.Column<int>(nullable: false),
                    TypeId = table.Column<string>(maxLength: 20, nullable: false),
                    SubtypeId = table.Column<string>(maxLength: 20, nullable: true),
                    Reject = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 20, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(maxLength: 20, nullable: false),
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
                    ChangedBy = table.Column<string>(maxLength: 20, nullable: false),
                    Size = table.Column<int>(nullable: false),
                    Signed = table.Column<bool>(nullable: false),
                    FileId = table.Column<int>(nullable: true),
                    RowId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileVersion", x => x.VersionId);
                    table.ForeignKey(
                        name: "FK_FileVersion_Metadata_FileId",
                        column: x => x.FileId,
                        principalTable: "Metadata",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileVersion_FileContent_RowId",
                        column: x => x.RowId,
                        principalTable: "FileContent",
                        principalColumn: "RowId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileVersion_FileId",
                table: "FileVersion",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileVersion_RowId",
                table: "FileVersion",
                column: "RowId",
                unique: true,
                filter: "[RowId] IS NOT NULL");
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

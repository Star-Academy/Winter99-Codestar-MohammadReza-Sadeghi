using Microsoft.EntityFrameworkCore.Migrations;

namespace Phase05.Migrations
{
    public partial class InvertedIndexDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocId);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "WordDocs",
                columns: table => new
                {
                    WordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DocId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordDocs", x => new { x.WordId, x.DocId });
                    table.ForeignKey(
                        name: "FK_WordDocs_Documents_DocId",
                        column: x => x.DocId,
                        principalTable: "Documents",
                        principalColumn: "DocId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordDocs_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordDocs_DocId",
                table: "WordDocs",
                column: "DocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordDocs");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}

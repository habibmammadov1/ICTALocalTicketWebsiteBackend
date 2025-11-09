using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ICTAWebsiteMig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BASE_RULES_FILES_PHOTOS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FILE_PATH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RULE_ITEM_ID = table.Column<int>(type: "int", nullable: false),
                    FILE_OR_PHOTO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASE_RULES_FILES_PHOTOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BASE_RULES_FILES_PHOTOS_BASE_RULES_RULE_ITEM_ID",
                        column: x => x.RULE_ITEM_ID,
                        principalTable: "BASE_RULES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BASE_RULES_FILES_PHOTOS_RULE_ITEM_ID",
                table: "BASE_RULES_FILES_PHOTOS",
                column: "RULE_ITEM_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BASE_RULES_FILES_PHOTOS");
        }
    }
}

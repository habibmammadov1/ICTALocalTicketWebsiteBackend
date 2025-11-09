using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ICTAWebsiteMig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BASE_RULES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BASE_RULE_ID = table.Column<int>(type: "int", nullable: false),
                    TITLE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CONTENT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COVER_PHOTO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HASHTAGS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATE_TIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATE_TIME = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CREATED_BY = table.Column<int>(type: "int", nullable: false),
                    UPDATED_BY = table.Column<int>(type: "int", nullable: true),
                    VIEW_COUNT = table.Column<int>(type: "int", nullable: false),
                    STATUS = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASE_RULES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QUESTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ANSWER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BASE_RULES");

            migrationBuilder.DropTable(
                name: "FAQ");
        }
    }
}

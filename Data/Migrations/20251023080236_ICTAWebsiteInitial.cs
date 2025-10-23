using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ICTAWebsiteInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITLE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CONTENT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COVER_PHOTO_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AUTHOR_ID = table.Column<int>(type: "int", nullable: false),
                    VIEW_COUNT = table.Column<int>(type: "int", nullable: false),
                    LIKE_COUNT = table.Column<int>(type: "int", nullable: false),
                    DISLIKE_COUNT = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITLE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CONTENT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COVER_PHOTO_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AUTHOR_ID = table.Column<int>(type: "int", nullable: false),
                    VIEW_COUNT = table.Column<int>(type: "int", nullable: false),
                    LIKE_COUNT = table.Column<int>(type: "int", nullable: false),
                    DISLIKE_COUNT = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AUTH",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    F_NAME = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DEPARTMENT_NAME = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    MAIL_TOKEN = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    USERNAME = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    STATUS = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Footer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DATE_YEAR = table.Column<int>(type: "int", nullable: true),
                    ROOM_NO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TECHNICAL_PHONE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MADE_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LAST_UPDATED = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITLE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CONTENT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COVER_PHOTO_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AUTHOR_ID = table.Column<int>(type: "int", nullable: false),
                    VIEW_COUNT = table.Column<int>(type: "int", nullable: false),
                    LIKE_COUNT = table.Column<int>(type: "int", nullable: false),
                    DISLIKE_COUNT = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Regulations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITLE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PHOTO_PATH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FILE_PATH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LAST_UPDATED = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regulations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    SURNAME = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    POSITION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PHOTO_PATH = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PHONE = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LINKEDIN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    STATUS = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AUTH");

            migrationBuilder.DropTable(
                name: "Footer");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Regulations");

            migrationBuilder.DropTable(
                name: "TeamMembers");
        }
    }
}

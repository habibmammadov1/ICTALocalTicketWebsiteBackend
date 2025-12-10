using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ICTAWebsiteInitial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DISLIKE_COUNT",
                table: "NOVELTIES");

            migrationBuilder.DropColumn(
                name: "LIKE_COUNT",
                table: "NOVELTIES");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DISLIKE_COUNT",
                table: "NOVELTIES",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LIKE_COUNT",
                table: "NOVELTIES",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

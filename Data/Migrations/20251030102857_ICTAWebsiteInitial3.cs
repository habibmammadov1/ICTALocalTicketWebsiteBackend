using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ICTAWebsiteInitial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NEWS_ID",
                table: "NOVELTIES_LIKE_DISLIKE");

            migrationBuilder.RenameColumn(
                name: "NoveltyId",
                table: "NOVELTIES_LIKE_DISLIKE",
                newName: "NOVELTY_ITEM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOVELTIES_LIKE_DISLIKE_NOVELTY_ITEM_ID",
                table: "NOVELTIES_LIKE_DISLIKE",
                column: "NOVELTY_ITEM_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_NOVELTIES_LIKE_DISLIKE_NOVELTIES_NOVELTY_ITEM_ID",
                table: "NOVELTIES_LIKE_DISLIKE",
                column: "NOVELTY_ITEM_ID",
                principalTable: "NOVELTIES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NOVELTIES_LIKE_DISLIKE_NOVELTIES_NOVELTY_ITEM_ID",
                table: "NOVELTIES_LIKE_DISLIKE");

            migrationBuilder.DropIndex(
                name: "IX_NOVELTIES_LIKE_DISLIKE_NOVELTY_ITEM_ID",
                table: "NOVELTIES_LIKE_DISLIKE");

            migrationBuilder.RenameColumn(
                name: "NOVELTY_ITEM_ID",
                table: "NOVELTIES_LIKE_DISLIKE",
                newName: "NoveltyId");

            migrationBuilder.AddColumn<int>(
                name: "NEWS_ID",
                table: "NOVELTIES_LIKE_DISLIKE",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

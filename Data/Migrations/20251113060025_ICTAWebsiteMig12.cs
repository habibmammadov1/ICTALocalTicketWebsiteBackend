using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ICTAWebsiteMig12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DESCRIPTION",
                table: "MEETINGS",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AUTHOR_ID",
                table: "MEETINGS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "END_TIME",
                table: "MEETINGS",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PERIOD",
                table: "MEETINGS",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AUTHOR_ID",
                table: "MEETINGS");

            migrationBuilder.DropColumn(
                name: "END_TIME",
                table: "MEETINGS");

            migrationBuilder.DropColumn(
                name: "PERIOD",
                table: "MEETINGS");

            migrationBuilder.AlterColumn<string>(
                name: "DESCRIPTION",
                table: "MEETINGS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}

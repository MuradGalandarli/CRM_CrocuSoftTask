using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Layer.Migrations
{
    /// <inheritdoc />
    public partial class deleteStatusCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "Reaports");

            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "Members");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "Reaports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

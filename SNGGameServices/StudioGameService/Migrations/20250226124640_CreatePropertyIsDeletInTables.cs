using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioGameService.Migrations
{
    /// <inheritdoc />
    public partial class CreatePropertyIsDeletInTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelet",
                table: "Tags",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelet",
                table: "Studios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelet",
                table: "JobsInStudio",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelet",
                table: "Genres",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelet",
                table: "Games",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelet",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "IsDelet",
                table: "Studios");

            migrationBuilder.DropColumn(
                name: "IsDelet",
                table: "JobsInStudio");

            migrationBuilder.DropColumn(
                name: "IsDelet",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "IsDelet",
                table: "Games");
        }
    }
}

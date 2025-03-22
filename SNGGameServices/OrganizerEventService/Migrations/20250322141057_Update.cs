using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizerEventService.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilepathToDescription",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FilepathToPhotoIcon",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PublishingStatus",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Events",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "FilepathToDescription",
                table: "Events",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilepathToPhotoIcon",
                table: "Events",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PublishingStatus",
                table: "Events",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

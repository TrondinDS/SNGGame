using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizerEventService.Migrations
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilepathToDescription",
                table: "EventOrganizers");

            migrationBuilder.DropColumn(
                name: "FilepathToPhotoIcon",
                table: "EventOrganizers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilepathToDescription",
                table: "EventOrganizers",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilepathToPhotoIcon",
                table: "EventOrganizers",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}

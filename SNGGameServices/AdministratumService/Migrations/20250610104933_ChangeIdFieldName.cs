using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdministratumService.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Messages",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Messages",
                newName: "id");
        }
    }
}

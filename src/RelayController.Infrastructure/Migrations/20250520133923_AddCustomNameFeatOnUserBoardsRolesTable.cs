using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelayController.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomNameFeatOnUserBoardsRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomName",
                table: "user_boards_roles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomName",
                table: "user_boards_roles");
        }
    }
}

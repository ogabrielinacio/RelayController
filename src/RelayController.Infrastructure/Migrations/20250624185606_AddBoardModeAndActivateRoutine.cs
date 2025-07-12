using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelayController.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBoardModeAndActivateRoutine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "relay_controller_routines",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "mode",
                table: "relay_controller_boards",
                type: "text",
                nullable: false,
                defaultValue: "Auto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "relay_controller_routines");

            migrationBuilder.DropColumn(
                name: "mode",
                table: "relay_controller_boards");
        }
    }
}

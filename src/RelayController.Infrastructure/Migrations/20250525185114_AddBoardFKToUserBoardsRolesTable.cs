using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelayController.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBoardFKToUserBoardsRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_boards_roles_user_id",
                table: "user_boards_roles");

            migrationBuilder.RenameColumn(
                name: "relay_controller_id",
                table: "user_boards_roles",
                newName: "relay_controller_board_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_boards_roles_relay_controller_board_id",
                table: "user_boards_roles",
                column: "relay_controller_board_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_boards_roles_user_id_relay_controller_board_id",
                table: "user_boards_roles",
                columns: new[] { "user_id", "relay_controller_board_id" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_user_boards_roles_relay_controller_boards_relay_controller_~",
                table: "user_boards_roles",
                column: "relay_controller_board_id",
                principalTable: "relay_controller_boards",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_boards_roles_relay_controller_boards_relay_controller_~",
                table: "user_boards_roles");

            migrationBuilder.DropIndex(
                name: "IX_user_boards_roles_relay_controller_board_id",
                table: "user_boards_roles");

            migrationBuilder.DropIndex(
                name: "IX_user_boards_roles_user_id_relay_controller_board_id",
                table: "user_boards_roles");

            migrationBuilder.RenameColumn(
                name: "relay_controller_board_id",
                table: "user_boards_roles",
                newName: "relay_controller_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_boards_roles_user_id",
                table: "user_boards_roles",
                column: "user_id");
        }
    }
}

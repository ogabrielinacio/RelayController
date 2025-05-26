using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelayController.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoutines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "day_of_month",
                table: "relay_controller_boards");

            migrationBuilder.DropColumn(
                name: "day_of_week",
                table: "relay_controller_boards");

            migrationBuilder.DropColumn(
                name: "end_hour",
                table: "relay_controller_boards");

            migrationBuilder.DropColumn(
                name: "end_minute",
                table: "relay_controller_boards");

            migrationBuilder.DropColumn(
                name: "end_second",
                table: "relay_controller_boards");

            migrationBuilder.DropColumn(
                name: "repeat",
                table: "relay_controller_boards");

            migrationBuilder.DropColumn(
                name: "start_hour",
                table: "relay_controller_boards");

            migrationBuilder.DropColumn(
                name: "start_minute",
                table: "relay_controller_boards");

            migrationBuilder.DropColumn(
                name: "start_second",
                table: "relay_controller_boards");

            migrationBuilder.CreateTable(
                name: "relay_controller_routines",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    relay_controller_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_hour = table.Column<int>(type: "integer", nullable: false),
                    start_minute = table.Column<int>(type: "integer", nullable: false),
                    start_second = table.Column<int>(type: "integer", nullable: false),
                    end_hour = table.Column<int>(type: "integer", nullable: true),
                    end_minute = table.Column<int>(type: "integer", nullable: true),
                    end_second = table.Column<int>(type: "integer", nullable: true),
                    repeat = table.Column<string>(type: "text", nullable: false),
                    day_of_week = table.Column<string>(type: "text", nullable: true),
                    day_of_month = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relay_controller_routines", x => x.id);
                    table.ForeignKey(
                        name: "FK_relay_controller_routines_relay_controller_boards_relay_con~",
                        column: x => x.relay_controller_id,
                        principalTable: "relay_controller_boards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_relay_controller_routines_created_date",
                table: "relay_controller_routines",
                column: "created_date");

            migrationBuilder.CreateIndex(
                name: "IX_relay_controller_routines_relay_controller_id",
                table: "relay_controller_routines",
                column: "relay_controller_id");

            migrationBuilder.CreateIndex(
                name: "IX_relay_controller_routines_updated_date",
                table: "relay_controller_routines",
                column: "updated_date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "relay_controller_routines");

            migrationBuilder.AddColumn<int>(
                name: "day_of_month",
                table: "relay_controller_boards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "day_of_week",
                table: "relay_controller_boards",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "end_hour",
                table: "relay_controller_boards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "end_minute",
                table: "relay_controller_boards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "end_second",
                table: "relay_controller_boards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "repeat",
                table: "relay_controller_boards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "start_hour",
                table: "relay_controller_boards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "start_minute",
                table: "relay_controller_boards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "start_second",
                table: "relay_controller_boards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelayController.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "relay_controller_boards",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_enable = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_relay_controller_boards", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_relay_controller_boards_created_date",
                table: "relay_controller_boards",
                column: "created_date");

            migrationBuilder.CreateIndex(
                name: "IX_relay_controller_boards_updated_date",
                table: "relay_controller_boards",
                column: "updated_date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "relay_controller_boards");
        }
    }
}

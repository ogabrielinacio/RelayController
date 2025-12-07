using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelayController.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPowerStateChangedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "power_state_changed_at",
                table: "relay_controller_boards",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "power_state_changed_at",
                table: "relay_controller_boards");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelayController.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<byte[]>(type: "bytea", nullable: false),
                    password_salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_boards_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    relay_controller_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    role_name = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_boards_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_boards_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_boards_roles_created_date",
                table: "user_boards_roles",
                column: "created_date");

            migrationBuilder.CreateIndex(
                name: "IX_user_boards_roles_updated_date",
                table: "user_boards_roles",
                column: "updated_date");

            migrationBuilder.CreateIndex(
                name: "IX_user_boards_roles_user_id",
                table: "user_boards_roles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_created_date",
                table: "users",
                column: "created_date");

            migrationBuilder.CreateIndex(
                name: "IX_users_updated_date",
                table: "users",
                column: "updated_date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_boards_roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskInsightEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_Id",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "status",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "roles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "prioritys",
                newName: "PriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_UserId",
                table: "users",
                column: "UserId",
                principalTable: "roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_UserId",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "status",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PriorityId",
                table: "prioritys",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_Id",
                table: "users",
                column: "Id",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskInsightEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDailyTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectMemberId",
                table: "dailyTaskUpdateStatus",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_dailyTaskUpdateStatus_ProjectMemberId",
                table: "dailyTaskUpdateStatus",
                column: "ProjectMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_dailyTaskUpdateStatus_projectmembers_ProjectMemberId",
                table: "dailyTaskUpdateStatus",
                column: "ProjectMemberId",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dailyTaskUpdateStatus_projectmembers_ProjectMemberId",
                table: "dailyTaskUpdateStatus");

            migrationBuilder.DropIndex(
                name: "IX_dailyTaskUpdateStatus_ProjectMemberId",
                table: "dailyTaskUpdateStatus");

            migrationBuilder.DropColumn(
                name: "ProjectMemberId",
                table: "dailyTaskUpdateStatus");
        }
    }
}

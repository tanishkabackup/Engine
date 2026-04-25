using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskInsightEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class assignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_taskAssignment_TaskItemId",
                table: "taskAssignment");

            migrationBuilder.CreateIndex(
                name: "IX_taskAssignment_TaskItemId",
                table: "taskAssignment",
                column: "TaskItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_taskAssignment_TaskItemId",
                table: "taskAssignment");

            migrationBuilder.CreateIndex(
                name: "IX_taskAssignment_TaskItemId",
                table: "taskAssignment",
                column: "TaskItemId",
                unique: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskInsightEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TaskImpedimentCommentsProjectMemberUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_taskimpedimentcomment_CreatedBy",
                table: "taskimpedimentcomment",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_taskimpedimentcomment_projectmembers_CreatedBy",
                table: "taskimpedimentcomment",
                column: "CreatedBy",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_taskimpedimentcomment_projectmembers_CreatedBy",
                table: "taskimpedimentcomment");

            migrationBuilder.DropIndex(
                name: "IX_taskimpedimentcomment_CreatedBy",
                table: "taskimpedimentcomment");
        }
    }
}

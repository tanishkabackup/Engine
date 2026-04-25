using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskInsightEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TaskAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "taskAssignments",
                columns: table => new
                {
                    TaskAssignmentId = table.Column<int>(type: "integer", nullable: false),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    Assignee = table.Column<int>(type: "integer", nullable: false),
                    Assigner = table.Column<int>(type: "integer", nullable: false),
                    ManagerId = table.Column<int>(type: "integer", nullable: false),
                    OpenedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskAssignments", x => x.TaskAssignmentId);
                    table.ForeignKey(
                        name: "FK_taskAssignments_projectmembers_Assignee",
                        column: x => x.Assignee,
                        principalTable: "projectmembers",
                        principalColumn: "ProjectMemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_taskAssignments_projectmembers_Assigner",
                        column: x => x.Assigner,
                        principalTable: "projectmembers",
                        principalColumn: "ProjectMemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_taskAssignments_projectmembers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "projectmembers",
                        principalColumn: "ProjectMemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_taskAssignments_taskItem_TaskAssignmentId",
                        column: x => x.TaskAssignmentId,
                        principalTable: "taskItem",
                        principalColumn: "TaskItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_taskAssignments_Assignee",
                table: "taskAssignments",
                column: "Assignee");

            migrationBuilder.CreateIndex(
                name: "IX_taskAssignments_Assigner",
                table: "taskAssignments",
                column: "Assigner");

            migrationBuilder.CreateIndex(
                name: "IX_taskAssignments_ManagerId",
                table: "taskAssignments",
                column: "ManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "taskAssignments");
        }
    }
}

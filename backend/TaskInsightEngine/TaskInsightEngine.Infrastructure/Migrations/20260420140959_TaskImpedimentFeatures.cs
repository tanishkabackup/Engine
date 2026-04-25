using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskInsightEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TaskImpedimentFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_taskimpediment_taskAssignment_TaskAssignmentId",
                table: "taskimpediment");

            migrationBuilder.DropIndex(
                name: "IX_taskimpediment_TaskAssignmentId",
                table: "taskimpediment");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "taskimpediment");

            migrationBuilder.DropColumn(
                name: "TaskAssignmentId",
                table: "taskimpediment");

            migrationBuilder.CreateTable(
                name: "TaskAssignmentTaskImpediment",
                columns: table => new
                {
                    TaskAssignmentsTaskAssignmentId = table.Column<int>(type: "integer", nullable: false),
                    TaskImpedimentsTaskImpedimentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAssignmentTaskImpediment", x => new { x.TaskAssignmentsTaskAssignmentId, x.TaskImpedimentsTaskImpedimentId });
                    table.ForeignKey(
                        name: "FK_TaskAssignmentTaskImpediment_taskAssignment_TaskAssignments~",
                        column: x => x.TaskAssignmentsTaskAssignmentId,
                        principalTable: "taskAssignment",
                        principalColumn: "TaskAssignmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskAssignmentTaskImpediment_taskimpediment_TaskImpediments~",
                        column: x => x.TaskImpedimentsTaskImpedimentId,
                        principalTable: "taskimpediment",
                        principalColumn: "TaskImpedimentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignmentTaskImpediment_TaskImpedimentsTaskImpedimentId",
                table: "TaskAssignmentTaskImpediment",
                column: "TaskImpedimentsTaskImpedimentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskAssignmentTaskImpediment");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "taskimpediment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TaskAssignmentId",
                table: "taskimpediment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_taskimpediment_TaskAssignmentId",
                table: "taskimpediment",
                column: "TaskAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_taskimpediment_taskAssignment_TaskAssignmentId",
                table: "taskimpediment",
                column: "TaskAssignmentId",
                principalTable: "taskAssignment",
                principalColumn: "TaskAssignmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskInsightEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TaskAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_taskAssignments_projectmembers_Assignee",
                table: "taskAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_taskAssignments_projectmembers_Assigner",
                table: "taskAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_taskAssignments_projectmembers_ManagerId",
                table: "taskAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_taskAssignments_taskItem_TaskAssignmentId",
                table: "taskAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_taskAssignments",
                table: "taskAssignments");

            migrationBuilder.RenameTable(
                name: "taskAssignments",
                newName: "taskAssignment");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "taskAssignment",
                newName: "TaskItemId");

            migrationBuilder.RenameColumn(
                name: "OpenedDate",
                table: "taskAssignment",
                newName: "OpeningDate");

            migrationBuilder.RenameColumn(
                name: "Assigner",
                table: "taskAssignment",
                newName: "AssignerId");

            migrationBuilder.RenameColumn(
                name: "Assignee",
                table: "taskAssignment",
                newName: "AssigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_taskAssignments_ManagerId",
                table: "taskAssignment",
                newName: "IX_taskAssignment_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_taskAssignments_Assigner",
                table: "taskAssignment",
                newName: "IX_taskAssignment_AssignerId");

            migrationBuilder.RenameIndex(
                name: "IX_taskAssignments_Assignee",
                table: "taskAssignment",
                newName: "IX_taskAssignment_AssigneeId");

            migrationBuilder.AlterColumn<int>(
                name: "TaskAssignmentId",
                table: "taskAssignment",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_taskAssignment",
                table: "taskAssignment",
                column: "TaskAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_taskAssignment_TaskItemId",
                table: "taskAssignment",
                column: "TaskItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_taskAssignment_projectmembers_AssigneeId",
                table: "taskAssignment",
                column: "AssigneeId",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskAssignment_projectmembers_AssignerId",
                table: "taskAssignment",
                column: "AssignerId",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskAssignment_projectmembers_ManagerId",
                table: "taskAssignment",
                column: "ManagerId",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskAssignment_taskItem_TaskItemId",
                table: "taskAssignment",
                column: "TaskItemId",
                principalTable: "taskItem",
                principalColumn: "TaskItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_taskAssignment_projectmembers_AssigneeId",
                table: "taskAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_taskAssignment_projectmembers_AssignerId",
                table: "taskAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_taskAssignment_projectmembers_ManagerId",
                table: "taskAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_taskAssignment_taskItem_TaskItemId",
                table: "taskAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_taskAssignment",
                table: "taskAssignment");

            migrationBuilder.DropIndex(
                name: "IX_taskAssignment_TaskItemId",
                table: "taskAssignment");

            migrationBuilder.RenameTable(
                name: "taskAssignment",
                newName: "taskAssignments");

            migrationBuilder.RenameColumn(
                name: "TaskItemId",
                table: "taskAssignments",
                newName: "TaskId");

            migrationBuilder.RenameColumn(
                name: "OpeningDate",
                table: "taskAssignments",
                newName: "OpenedDate");

            migrationBuilder.RenameColumn(
                name: "AssignerId",
                table: "taskAssignments",
                newName: "Assigner");

            migrationBuilder.RenameColumn(
                name: "AssigneeId",
                table: "taskAssignments",
                newName: "Assignee");

            migrationBuilder.RenameIndex(
                name: "IX_taskAssignment_ManagerId",
                table: "taskAssignments",
                newName: "IX_taskAssignments_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_taskAssignment_AssignerId",
                table: "taskAssignments",
                newName: "IX_taskAssignments_Assigner");

            migrationBuilder.RenameIndex(
                name: "IX_taskAssignment_AssigneeId",
                table: "taskAssignments",
                newName: "IX_taskAssignments_Assignee");

            migrationBuilder.AlterColumn<int>(
                name: "TaskAssignmentId",
                table: "taskAssignments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_taskAssignments",
                table: "taskAssignments",
                column: "TaskAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_taskAssignments_projectmembers_Assignee",
                table: "taskAssignments",
                column: "Assignee",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskAssignments_projectmembers_Assigner",
                table: "taskAssignments",
                column: "Assigner",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskAssignments_projectmembers_ManagerId",
                table: "taskAssignments",
                column: "ManagerId",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskAssignments_taskItem_TaskAssignmentId",
                table: "taskAssignments",
                column: "TaskAssignmentId",
                principalTable: "taskItem",
                principalColumn: "TaskItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

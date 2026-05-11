using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskInsightEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameMultipleTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dailyTaskUpdateStatus_projectmembers_ProjectMemberId",
                table: "dailyTaskUpdateStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_dailyTaskUpdateStatus_status_StatusId",
                table: "dailyTaskUpdateStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_dailyTaskUpdateStatus_taskItem_TaskId",
                table: "dailyTaskUpdateStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_risksnapshot_taskItem_TaskId",
                table: "risksnapshot");

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

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignmentTaskImpediment_taskAssignment_TaskAssignments~",
                table: "TaskAssignmentTaskImpediment");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignmentTaskImpediment_taskimpediment_TaskImpediments~",
                table: "TaskAssignmentTaskImpediment");

            migrationBuilder.DropForeignKey(
                name: "FK_taskimpediment_risk_RiskId",
                table: "taskimpediment");

            migrationBuilder.DropForeignKey(
                name: "FK_taskimpediment_taskItem_TaskItemId",
                table: "taskimpediment");

            migrationBuilder.DropForeignKey(
                name: "FK_taskimpedimentcomment_projectmembers_CreatedBy",
                table: "taskimpedimentcomment");

            migrationBuilder.DropForeignKey(
                name: "FK_taskimpedimentcomment_taskimpediment_TaskImpedimentId",
                table: "taskimpedimentcomment");

            migrationBuilder.DropForeignKey(
                name: "FK_taskItem_prioritys_PriorityId",
                table: "taskItem");

            migrationBuilder.DropForeignKey(
                name: "FK_taskItem_projects_ProjectId",
                table: "taskItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskSubscriptions",
                table: "RiskSubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dailyTaskUpdateStatus",
                table: "dailyTaskUpdateStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_taskItem",
                table: "taskItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_taskimpedimentcomment",
                table: "taskimpedimentcomment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_taskimpediment",
                table: "taskimpediment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_taskAssignment",
                table: "taskAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_risksnapshot",
                table: "risksnapshot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_briefing",
                table: "briefing");

            migrationBuilder.RenameTable(
                name: "RiskSubscriptions",
                newName: "risksubscriptions");

            migrationBuilder.RenameTable(
                name: "dailyTaskUpdateStatus",
                newName: "dailytaskupdatestatus");

            migrationBuilder.RenameTable(
                name: "taskItem",
                newName: "taskItems");

            migrationBuilder.RenameTable(
                name: "taskimpedimentcomment",
                newName: "taskimpedimentcomments");

            migrationBuilder.RenameTable(
                name: "taskimpediment",
                newName: "taskimpediments");

            migrationBuilder.RenameTable(
                name: "taskAssignment",
                newName: "taskassignments");

            migrationBuilder.RenameTable(
                name: "risksnapshot",
                newName: "risksnapshots");

            migrationBuilder.RenameTable(
                name: "briefing",
                newName: "briefings");

            migrationBuilder.RenameIndex(
                name: "IX_RiskSubscriptions_UserEmail_ProjectId",
                table: "risksubscriptions",
                newName: "IX_risksubscriptions_UserEmail_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_RiskSubscriptions_UserEmail",
                table: "risksubscriptions",
                newName: "IX_risksubscriptions_UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_dailyTaskUpdateStatus_TaskId",
                table: "dailytaskupdatestatus",
                newName: "IX_dailytaskupdatestatus_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_dailyTaskUpdateStatus_StatusId",
                table: "dailytaskupdatestatus",
                newName: "IX_dailytaskupdatestatus_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_dailyTaskUpdateStatus_ProjectMemberId",
                table: "dailytaskupdatestatus",
                newName: "IX_dailytaskupdatestatus_ProjectMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_taskItem_ProjectId",
                table: "taskItems",
                newName: "IX_taskItems_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_taskItem_PriorityId",
                table: "taskItems",
                newName: "IX_taskItems_PriorityId");

            migrationBuilder.RenameIndex(
                name: "IX_taskimpedimentcomment_TaskImpedimentId",
                table: "taskimpedimentcomments",
                newName: "IX_taskimpedimentcomments_TaskImpedimentId");

            migrationBuilder.RenameIndex(
                name: "IX_taskimpedimentcomment_CreatedBy",
                table: "taskimpedimentcomments",
                newName: "IX_taskimpedimentcomments_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_taskimpediment_TaskItemId",
                table: "taskimpediments",
                newName: "IX_taskimpediments_TaskItemId");

            migrationBuilder.RenameIndex(
                name: "IX_taskimpediment_RiskId",
                table: "taskimpediments",
                newName: "IX_taskimpediments_RiskId");

            migrationBuilder.RenameIndex(
                name: "IX_taskAssignment_TaskItemId",
                table: "taskassignments",
                newName: "IX_taskassignments_TaskItemId");

            migrationBuilder.RenameIndex(
                name: "IX_taskAssignment_ManagerId",
                table: "taskassignments",
                newName: "IX_taskassignments_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_taskAssignment_AssignerId",
                table: "taskassignments",
                newName: "IX_taskassignments_AssignerId");

            migrationBuilder.RenameIndex(
                name: "IX_taskAssignment_AssigneeId",
                table: "taskassignments",
                newName: "IX_taskassignments_AssigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_briefing_SilentCount",
                table: "briefings",
                newName: "IX_briefings_SilentCount");

            migrationBuilder.RenameIndex(
                name: "IX_briefing_RecoveringCount",
                table: "briefings",
                newName: "IX_briefings_RecoveringCount");

            migrationBuilder.RenameIndex(
                name: "IX_briefing_CreatedAt",
                table: "briefings",
                newName: "IX_briefings_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_briefing_AttentionCount",
                table: "briefings",
                newName: "IX_briefings_AttentionCount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_risksubscriptions",
                table: "risksubscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dailytaskupdatestatus",
                table: "dailytaskupdatestatus",
                column: "DailyTaskUpdateStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_taskItems",
                table: "taskItems",
                column: "TaskItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_taskimpedimentcomments",
                table: "taskimpedimentcomments",
                column: "TaskImpedimentCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_taskimpediments",
                table: "taskimpediments",
                column: "TaskImpedimentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_taskassignments",
                table: "taskassignments",
                column: "TaskAssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_risksnapshots",
                table: "risksnapshots",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_briefings",
                table: "briefings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dailytaskupdatestatus_projectmembers_ProjectMemberId",
                table: "dailytaskupdatestatus",
                column: "ProjectMemberId",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dailytaskupdatestatus_status_StatusId",
                table: "dailytaskupdatestatus",
                column: "StatusId",
                principalTable: "status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dailytaskupdatestatus_taskItems_TaskId",
                table: "dailytaskupdatestatus",
                column: "TaskId",
                principalTable: "taskItems",
                principalColumn: "TaskItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_risksnapshots_taskItems_TaskId",
                table: "risksnapshots",
                column: "TaskId",
                principalTable: "taskItems",
                principalColumn: "TaskItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskassignments_projectmembers_AssigneeId",
                table: "taskassignments",
                column: "AssigneeId",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskassignments_projectmembers_AssignerId",
                table: "taskassignments",
                column: "AssignerId",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskassignments_projectmembers_ManagerId",
                table: "taskassignments",
                column: "ManagerId",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskassignments_taskItems_TaskItemId",
                table: "taskassignments",
                column: "TaskItemId",
                principalTable: "taskItems",
                principalColumn: "TaskItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignmentTaskImpediment_taskassignments_TaskAssignment~",
                table: "TaskAssignmentTaskImpediment",
                column: "TaskAssignmentsTaskAssignmentId",
                principalTable: "taskassignments",
                principalColumn: "TaskAssignmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignmentTaskImpediment_taskimpediments_TaskImpediment~",
                table: "TaskAssignmentTaskImpediment",
                column: "TaskImpedimentsTaskImpedimentId",
                principalTable: "taskimpediments",
                principalColumn: "TaskImpedimentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskimpedimentcomments_projectmembers_CreatedBy",
                table: "taskimpedimentcomments",
                column: "CreatedBy",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskimpedimentcomments_taskimpediments_TaskImpedimentId",
                table: "taskimpedimentcomments",
                column: "TaskImpedimentId",
                principalTable: "taskimpediments",
                principalColumn: "TaskImpedimentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskimpediments_risk_RiskId",
                table: "taskimpediments",
                column: "RiskId",
                principalTable: "risk",
                principalColumn: "RiskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskimpediments_taskItems_TaskItemId",
                table: "taskimpediments",
                column: "TaskItemId",
                principalTable: "taskItems",
                principalColumn: "TaskItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskItems_prioritys_PriorityId",
                table: "taskItems",
                column: "PriorityId",
                principalTable: "prioritys",
                principalColumn: "PriorityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskItems_projects_ProjectId",
                table: "taskItems",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dailytaskupdatestatus_projectmembers_ProjectMemberId",
                table: "dailytaskupdatestatus");

            migrationBuilder.DropForeignKey(
                name: "FK_dailytaskupdatestatus_status_StatusId",
                table: "dailytaskupdatestatus");

            migrationBuilder.DropForeignKey(
                name: "FK_dailytaskupdatestatus_taskItems_TaskId",
                table: "dailytaskupdatestatus");

            migrationBuilder.DropForeignKey(
                name: "FK_risksnapshots_taskItems_TaskId",
                table: "risksnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_taskassignments_projectmembers_AssigneeId",
                table: "taskassignments");

            migrationBuilder.DropForeignKey(
                name: "FK_taskassignments_projectmembers_AssignerId",
                table: "taskassignments");

            migrationBuilder.DropForeignKey(
                name: "FK_taskassignments_projectmembers_ManagerId",
                table: "taskassignments");

            migrationBuilder.DropForeignKey(
                name: "FK_taskassignments_taskItems_TaskItemId",
                table: "taskassignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignmentTaskImpediment_taskassignments_TaskAssignment~",
                table: "TaskAssignmentTaskImpediment");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignmentTaskImpediment_taskimpediments_TaskImpediment~",
                table: "TaskAssignmentTaskImpediment");

            migrationBuilder.DropForeignKey(
                name: "FK_taskimpedimentcomments_projectmembers_CreatedBy",
                table: "taskimpedimentcomments");

            migrationBuilder.DropForeignKey(
                name: "FK_taskimpedimentcomments_taskimpediments_TaskImpedimentId",
                table: "taskimpedimentcomments");

            migrationBuilder.DropForeignKey(
                name: "FK_taskimpediments_risk_RiskId",
                table: "taskimpediments");

            migrationBuilder.DropForeignKey(
                name: "FK_taskimpediments_taskItems_TaskItemId",
                table: "taskimpediments");

            migrationBuilder.DropForeignKey(
                name: "FK_taskItems_prioritys_PriorityId",
                table: "taskItems");

            migrationBuilder.DropForeignKey(
                name: "FK_taskItems_projects_ProjectId",
                table: "taskItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_risksubscriptions",
                table: "risksubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dailytaskupdatestatus",
                table: "dailytaskupdatestatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_taskItems",
                table: "taskItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_taskimpediments",
                table: "taskimpediments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_taskimpedimentcomments",
                table: "taskimpedimentcomments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_taskassignments",
                table: "taskassignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_risksnapshots",
                table: "risksnapshots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_briefings",
                table: "briefings");

            migrationBuilder.RenameTable(
                name: "risksubscriptions",
                newName: "RiskSubscriptions");

            migrationBuilder.RenameTable(
                name: "dailytaskupdatestatus",
                newName: "dailyTaskUpdateStatus");

            migrationBuilder.RenameTable(
                name: "taskItems",
                newName: "taskItem");

            migrationBuilder.RenameTable(
                name: "taskimpediments",
                newName: "taskimpediment");

            migrationBuilder.RenameTable(
                name: "taskimpedimentcomments",
                newName: "taskimpedimentcomment");

            migrationBuilder.RenameTable(
                name: "taskassignments",
                newName: "taskAssignment");

            migrationBuilder.RenameTable(
                name: "risksnapshots",
                newName: "risksnapshot");

            migrationBuilder.RenameTable(
                name: "briefings",
                newName: "briefing");

            migrationBuilder.RenameIndex(
                name: "IX_risksubscriptions_UserEmail_ProjectId",
                table: "RiskSubscriptions",
                newName: "IX_RiskSubscriptions_UserEmail_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_risksubscriptions_UserEmail",
                table: "RiskSubscriptions",
                newName: "IX_RiskSubscriptions_UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_dailytaskupdatestatus_TaskId",
                table: "dailyTaskUpdateStatus",
                newName: "IX_dailyTaskUpdateStatus_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_dailytaskupdatestatus_StatusId",
                table: "dailyTaskUpdateStatus",
                newName: "IX_dailyTaskUpdateStatus_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_dailytaskupdatestatus_ProjectMemberId",
                table: "dailyTaskUpdateStatus",
                newName: "IX_dailyTaskUpdateStatus_ProjectMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_taskItems_ProjectId",
                table: "taskItem",
                newName: "IX_taskItem_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_taskItems_PriorityId",
                table: "taskItem",
                newName: "IX_taskItem_PriorityId");

            migrationBuilder.RenameIndex(
                name: "IX_taskimpediments_TaskItemId",
                table: "taskimpediment",
                newName: "IX_taskimpediment_TaskItemId");

            migrationBuilder.RenameIndex(
                name: "IX_taskimpediments_RiskId",
                table: "taskimpediment",
                newName: "IX_taskimpediment_RiskId");

            migrationBuilder.RenameIndex(
                name: "IX_taskimpedimentcomments_TaskImpedimentId",
                table: "taskimpedimentcomment",
                newName: "IX_taskimpedimentcomment_TaskImpedimentId");

            migrationBuilder.RenameIndex(
                name: "IX_taskimpedimentcomments_CreatedBy",
                table: "taskimpedimentcomment",
                newName: "IX_taskimpedimentcomment_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_taskassignments_TaskItemId",
                table: "taskAssignment",
                newName: "IX_taskAssignment_TaskItemId");

            migrationBuilder.RenameIndex(
                name: "IX_taskassignments_ManagerId",
                table: "taskAssignment",
                newName: "IX_taskAssignment_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_taskassignments_AssignerId",
                table: "taskAssignment",
                newName: "IX_taskAssignment_AssignerId");

            migrationBuilder.RenameIndex(
                name: "IX_taskassignments_AssigneeId",
                table: "taskAssignment",
                newName: "IX_taskAssignment_AssigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_briefings_SilentCount",
                table: "briefing",
                newName: "IX_briefing_SilentCount");

            migrationBuilder.RenameIndex(
                name: "IX_briefings_RecoveringCount",
                table: "briefing",
                newName: "IX_briefing_RecoveringCount");

            migrationBuilder.RenameIndex(
                name: "IX_briefings_CreatedAt",
                table: "briefing",
                newName: "IX_briefing_CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_briefings_AttentionCount",
                table: "briefing",
                newName: "IX_briefing_AttentionCount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskSubscriptions",
                table: "RiskSubscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dailyTaskUpdateStatus",
                table: "dailyTaskUpdateStatus",
                column: "DailyTaskUpdateStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_taskItem",
                table: "taskItem",
                column: "TaskItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_taskimpediment",
                table: "taskimpediment",
                column: "TaskImpedimentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_taskimpedimentcomment",
                table: "taskimpedimentcomment",
                column: "TaskImpedimentCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_taskAssignment",
                table: "taskAssignment",
                column: "TaskAssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_risksnapshot",
                table: "risksnapshot",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_briefing",
                table: "briefing",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dailyTaskUpdateStatus_projectmembers_ProjectMemberId",
                table: "dailyTaskUpdateStatus",
                column: "ProjectMemberId",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dailyTaskUpdateStatus_status_StatusId",
                table: "dailyTaskUpdateStatus",
                column: "StatusId",
                principalTable: "status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dailyTaskUpdateStatus_taskItem_TaskId",
                table: "dailyTaskUpdateStatus",
                column: "TaskId",
                principalTable: "taskItem",
                principalColumn: "TaskItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_risksnapshot_taskItem_TaskId",
                table: "risksnapshot",
                column: "TaskId",
                principalTable: "taskItem",
                principalColumn: "TaskItemId",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignmentTaskImpediment_taskAssignment_TaskAssignments~",
                table: "TaskAssignmentTaskImpediment",
                column: "TaskAssignmentsTaskAssignmentId",
                principalTable: "taskAssignment",
                principalColumn: "TaskAssignmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignmentTaskImpediment_taskimpediment_TaskImpediments~",
                table: "TaskAssignmentTaskImpediment",
                column: "TaskImpedimentsTaskImpedimentId",
                principalTable: "taskimpediment",
                principalColumn: "TaskImpedimentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskimpediment_risk_RiskId",
                table: "taskimpediment",
                column: "RiskId",
                principalTable: "risk",
                principalColumn: "RiskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskimpediment_taskItem_TaskItemId",
                table: "taskimpediment",
                column: "TaskItemId",
                principalTable: "taskItem",
                principalColumn: "TaskItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskimpedimentcomment_projectmembers_CreatedBy",
                table: "taskimpedimentcomment",
                column: "CreatedBy",
                principalTable: "projectmembers",
                principalColumn: "ProjectMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskimpedimentcomment_taskimpediment_TaskImpedimentId",
                table: "taskimpedimentcomment",
                column: "TaskImpedimentId",
                principalTable: "taskimpediment",
                principalColumn: "TaskImpedimentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskItem_prioritys_PriorityId",
                table: "taskItem",
                column: "PriorityId",
                principalTable: "prioritys",
                principalColumn: "PriorityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taskItem_projects_ProjectId",
                table: "taskItem",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

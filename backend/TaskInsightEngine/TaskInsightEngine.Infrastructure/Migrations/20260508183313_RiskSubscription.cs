using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskInsightEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RiskSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiskFactor");

            migrationBuilder.CreateTable(
                name: "briefing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AttentionCount = table.Column<int>(type: "integer", nullable: false),
                    SilentCount = table.Column<int>(type: "integer", nullable: false),
                    RecoveringCount = table.Column<int>(type: "integer", nullable: false),
                    BriefingSnapshot = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_briefing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "risksnapshot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PrevScore = table.Column<int>(type: "integer", nullable: true),
                    PrevLevel = table.Column<string>(type: "text", nullable: false),
                    MovementId = table.Column<int>(type: "integer", nullable: false),
                    Delta = table.Column<int>(type: "integer", nullable: true),
                    CurrentScore = table.Column<int>(type: "integer", nullable: true),
                    CurrentLevel = table.Column<string>(type: "text", nullable: false),
                    TopReasons = table.Column<string>(type: "text", nullable: false),
                    AssigneeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_risksnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_risksnapshot_taskItem_TaskId",
                        column: x => x.TaskId,
                        principalTable: "taskItem",
                        principalColumn: "TaskItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RiskSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskSubscriptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_briefing_AttentionCount",
                table: "briefing",
                column: "AttentionCount");

            migrationBuilder.CreateIndex(
                name: "IX_briefing_CreatedAt",
                table: "briefing",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_briefing_RecoveringCount",
                table: "briefing",
                column: "RecoveringCount");

            migrationBuilder.CreateIndex(
                name: "IX_briefing_SilentCount",
                table: "briefing",
                column: "SilentCount");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSnapshot_TaskId_Date_Id",
                table: "risksnapshot",
                columns: new[] { "TaskId", "Date", "Id" },
                descending: new[] { false, true, true });

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubscriptions_UserEmail",
                table: "RiskSubscriptions",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_RiskSubscriptions_UserEmail_ProjectId",
                table: "RiskSubscriptions",
                columns: new[] { "UserEmail", "ProjectId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "briefing");

            migrationBuilder.DropTable(
                name: "risksnapshot");

            migrationBuilder.DropTable(
                name: "RiskSubscriptions");

            migrationBuilder.CreateTable(
                name: "RiskFactor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BaseWeight = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    IsPerDay = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskFactor", x => x.Id);
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskInsightEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DailyTaskUpdateStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dailyTaskUpdateStatus",
                columns: table => new
                {
                    DailyTaskUpdateStatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    EffortHours = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedEta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dailyTaskUpdateStatus", x => x.DailyTaskUpdateStatusId);
                    table.ForeignKey(
                        name: "FK_dailyTaskUpdateStatus_status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dailyTaskUpdateStatus_taskItem_TaskId",
                        column: x => x.TaskId,
                        principalTable: "taskItem",
                        principalColumn: "TaskItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "status",
                columns: new[] { "StatusId", "Type" },
                values: new object[] { 6, "Blocked" });

            migrationBuilder.CreateIndex(
                name: "IX_dailyTaskUpdateStatus_StatusId",
                table: "dailyTaskUpdateStatus",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_dailyTaskUpdateStatus_TaskId",
                table: "dailyTaskUpdateStatus",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dailyTaskUpdateStatus");

            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "StatusId",
                keyValue: 6);
        }
    }
}

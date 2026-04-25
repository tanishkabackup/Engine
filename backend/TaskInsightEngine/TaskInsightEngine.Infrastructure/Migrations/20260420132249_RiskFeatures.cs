using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskInsightEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RiskFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "risk",
                columns: table => new
                {
                    RiskId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_risk", x => x.RiskId);
                });

            migrationBuilder.CreateTable(
                name: "taskimpediment",
                columns: table => new
                {
                    TaskImpedimentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskItemId = table.Column<int>(type: "integer", nullable: false),
                    RiskId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    ResolvedBy = table.Column<int>(type: "integer", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsResolved = table.Column<bool>(type: "boolean", nullable: false),
                    TaskAssignmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskimpediment", x => x.TaskImpedimentId);
                    table.ForeignKey(
                        name: "FK_taskimpediment_risk_RiskId",
                        column: x => x.RiskId,
                        principalTable: "risk",
                        principalColumn: "RiskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_taskimpediment_taskAssignment_TaskAssignmentId",
                        column: x => x.TaskAssignmentId,
                        principalTable: "taskAssignment",
                        principalColumn: "TaskAssignmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_taskimpediment_taskItem_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "taskItem",
                        principalColumn: "TaskItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "taskimpedimentcomment",
                columns: table => new
                {
                    TaskImpedimentCommentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskImpedimentId = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskimpedimentcomment", x => x.TaskImpedimentCommentId);
                    table.ForeignKey(
                        name: "FK_taskimpedimentcomment_taskimpediment_TaskImpedimentId",
                        column: x => x.TaskImpedimentId,
                        principalTable: "taskimpediment",
                        principalColumn: "TaskImpedimentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "risk",
                columns: new[] { "RiskId", "Description", "Name", "Weight" },
                values: new object[,]
                {
                    { 1, "Blocked by third-party/vendor or external system", "External Dependency", 3 },
                    { 2, "Blocked due to internal team dependency or issue", "Internal Blocker", 2 },
                    { 3, "Insufficient resources or bandwidth", "Resource Constraint", 3 },
                    { 4, "Unknown technical challenges or complexity", "Technical Uncertainty", 4 },
                    { 5, "Unclear or changing requirements", "Requirement Gap", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_taskimpediment_RiskId",
                table: "taskimpediment",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_taskimpediment_TaskAssignmentId",
                table: "taskimpediment",
                column: "TaskAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_taskimpediment_TaskItemId",
                table: "taskimpediment",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_taskimpedimentcomment_TaskImpedimentId",
                table: "taskimpedimentcomment",
                column: "TaskImpedimentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "taskimpedimentcomment");

            migrationBuilder.DropTable(
                name: "taskimpediment");

            migrationBuilder.DropTable(
                name: "risk");
        }
    }
}

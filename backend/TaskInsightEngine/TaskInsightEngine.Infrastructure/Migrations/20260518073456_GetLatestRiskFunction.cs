using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskInsightEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GetLatestRiskFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION get_latest_risk(project_id_param integer)
                RETURNS TABLE
                (
                    ""TaskId"" integer,
                    ""CurrentScore"" integer,
                    ""CurrentLevel"" text
                )
                AS
                $$
                BEGIN
                    RETURN QUERY
                    SELECT DISTINCT ON (r.""TaskId"")
                           r.""TaskId"",
                           r.""CurrentScore"",
                           r.""CurrentLevel""
                    FROM ""risksnapshots"" r
                    INNER JOIN ""taskItems"" t
                        ON t.""TaskItemId"" = r.""TaskId""
                    WHERE t.""ProjectId"" = project_id_param
                    ORDER BY r.""TaskId"", r.""Date"" DESC, r.""Id"" DESC;
                END;
                $$
                LANGUAGE plpgsql;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP FUNCTION IF EXISTS get_latest_risk(integer);
            ");
        }
    }
}
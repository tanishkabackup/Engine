CREATE OR REPLACE FUNCTION get_latest_risk(project_id_param bigint)
RETURNS TABLE
(
    "TaskId" integer,
    "CurrentScore" integer,
    "CurrentLevel" text
)
AS
$$
BEGIN
    RETURN QUERY
    SELECT DISTINCT ON (r."TaskId")
           r."TaskId",
           r."CurrentScore",
           r."CurrentLevel"
    FROM "risksnapshots" r
    INNER JOIN "taskItems" t
        ON t."TaskItemId" = r."TaskId"
    WHERE t."ProjectId" = project_id_param
    ORDER BY r."TaskId", r."Date" DESC, r."Id" DESC;
END;
$$
LANGUAGE plpgsql;
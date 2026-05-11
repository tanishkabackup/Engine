namespace TaskInsightEngine.Domain.Enums
{
    public static class RoleTypes
    {
        public const string ProjectManager = "ProjectManager";
        public const string Developer = "Developer";

        public static string MapIdToName(string? roleId)
        {
            return roleId switch
            {
                "1" => ProjectManager,
                "2" => Developer,
                _ => "Unknown"
            };

        }
    }
}

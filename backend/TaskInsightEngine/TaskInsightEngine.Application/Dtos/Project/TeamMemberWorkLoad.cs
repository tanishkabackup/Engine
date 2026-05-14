namespace TaskInsightEngine.Application.Dtos.Project
{
    public class TeamMemberWorkload
    {
        public string? AssigneeName { get; set; }
        public string? RoleName { get; set; }
        public int TotalItems { get; set; }
        public int Critical { get; set; }
        public int Warning { get; set; }
        public int Recovering { get; set; }
        public int Healthy { get; set; }
    }
}

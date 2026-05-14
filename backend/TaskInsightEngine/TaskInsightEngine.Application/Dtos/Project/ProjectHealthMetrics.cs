namespace TaskInsightEngine.Application.Dtos.Project
{
    public class ProjectHealthMetrics
    {
        public int TotalTasks { get; set; }
        public int Critical { get; set; }
        public int Warning { get; set; }
        public int Healthy { get; set; }
        public int Recovering { get; set; }
        public decimal HealthPercentage { get; set; }
    }
}

using TaskInsightEngine.Application.Dtos.Risk;

namespace TaskInsightEngine.Application.Dtos.Project
{
    public class FilterHealthyTaskResponse
    {
        public int TotalTaskCount { get; set; }
        public int HealthyTasksCount { get; set; }
        public decimal HealthyTaskPercentage { get; set; }
        public List<RiskSnapshotDto> HealthyTasks { get; set; } = [];
    }
}

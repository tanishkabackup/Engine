using TaskInsightEngine.Application.Dtos.Risk;

namespace TaskInsightEngine.Application.Dtos.Project
{
    public class ProjectMetricsDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public ProjectHealthMetrics? Metrics { get; set; }
        public BriefingSnapshotView? Groups { get; set; }
        public List<TeamMemberWorkload> Workload { get; set; } = [];
    }
}


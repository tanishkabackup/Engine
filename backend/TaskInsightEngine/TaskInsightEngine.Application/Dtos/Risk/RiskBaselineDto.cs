namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class RiskBaselineDto
    {
        public long TaskId { get; set; }
        public int CurrentScore { get; set; }
        public string? CurrentLevel { get; set; }
    }
}

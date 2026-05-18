namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class RiskBaselineDto
    {
        public int TaskId { get; set; }
        public int CurrentScore { get; set; }
        public string? CurrentLevel { get; set; }
    }
}

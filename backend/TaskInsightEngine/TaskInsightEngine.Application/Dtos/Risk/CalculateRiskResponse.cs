namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class CalculateRiskResponse
    {
        public int Score { get; set; }
        public string? Level { get; set; }
        public List<string?> Reasons { get; set; } = [];
        public bool IsHealthy { get; set; }
        public bool IsOverdue { get; set; }
        public bool HasBlockers { get; set; }
        public bool UpdatedToday { get; set; }
    }
}

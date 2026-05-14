namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class RiskSubscriptionDto
    {
        public int RiskSubscriptionId { get; set; }
        public string UserEmail { get; set; }
        public int ProjectId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string NextRun { get; set; }
        public string? ProjectName { get; set; }
        public string RiskSubscriptionGuid { get; set; }
    }
}

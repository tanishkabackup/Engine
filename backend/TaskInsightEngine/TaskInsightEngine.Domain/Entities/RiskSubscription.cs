namespace TaskInsightEngine.Domain.Entities
{
    public class RiskSubscription
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public int ProjectId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string NextRun { get; set; }
        public string RiskSubscriptionGuid { get; set; }
    }
}

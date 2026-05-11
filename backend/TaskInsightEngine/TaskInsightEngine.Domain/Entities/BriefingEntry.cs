namespace TaskInsightEngine.Domain.Entities
{
    public class BriefingEntry
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AttentionCount { get; set; }
        public int SilentCount { get; set; }
        public int RecoveringCount { get; set; }
        public BriefingSnapshot BriefingSnapshot { get; set; }
    }
}

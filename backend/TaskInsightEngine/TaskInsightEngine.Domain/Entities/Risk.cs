namespace TaskInsightEngine.Domain.Entities
{
    public class Risk
    {
        public int RiskId { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; }
    }
}

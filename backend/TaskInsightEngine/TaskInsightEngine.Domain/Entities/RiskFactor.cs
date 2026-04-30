namespace TaskInsightEngine.Domain.Entities
{
    public class RiskFactor
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int BaseWeight { get; set; }
        public bool IsPerDay { get; set; }
    }
}

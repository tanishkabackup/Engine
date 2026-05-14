namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class CreateProjectRiskSubscriptionRequest
    {
        public string Email { get; set; }
        public List<int> ProjectIds { get; set; } = [];
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }
}

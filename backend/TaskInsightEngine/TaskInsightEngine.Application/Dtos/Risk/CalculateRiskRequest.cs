namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class CalculateRiskRequest
    {
        public TaskRiskDetail Task { get; set; }
        public DateTime Today { get; set; }
    }
}

namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class ScheduleRiskDeliveryResponse
    {
        public string JobId { get; set; }
        public string CronExpression { get; set; }
        public string Schedule { get; set; }
    }
}

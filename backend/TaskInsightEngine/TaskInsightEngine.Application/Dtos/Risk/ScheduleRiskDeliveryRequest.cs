namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class ScheduleRiskDeliveryRequest
    {
       public string Email { get; set; }
       public int Hours { get; set; }
       public int Minutes { get; set; }
    }
}

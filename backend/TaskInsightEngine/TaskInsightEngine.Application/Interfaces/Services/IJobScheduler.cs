using TaskInsightEngine.Application.Dtos.Risk;

namespace TaskInsightEngine.Application.Interfaces.Services
{
    public interface IJobScheduler
    {
        public ScheduleRiskDeliveryResponse ScheduleRiskSubscriptionDelivery(ScheduleRiskDeliveryRequest request);
    }
}

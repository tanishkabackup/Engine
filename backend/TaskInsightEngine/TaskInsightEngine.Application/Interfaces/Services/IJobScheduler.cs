using TaskInsightEngine.Application.Dtos.Risk;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Interfaces.Services
{
    public interface IJobScheduler
    {
        public ScheduleRiskDeliveryResponse ScheduleRiskSubscriptionDelivery(ScheduleRiskDeliveryRequest request);
        void DeleteRiskSubscription(List<RiskSubscription> subscriptions);
    }
}

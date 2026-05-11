using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class GetRiskSubscriptionResponse
    {
        public List<RiskSubscription> Subscriptions { get; set; } = [];

    }
}

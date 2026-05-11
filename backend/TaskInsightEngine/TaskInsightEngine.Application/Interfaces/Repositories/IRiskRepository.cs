using TaskInsightEngine.Application.Dtos.Risk;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Interfaces.Repositories
{
    public interface IRiskRepository
    {
        Task AddRiskDetailsAsync(Risk risk);
        Task SaveRiskBriefingAsync(BriefingEntry entry);
        Task AddRiskSnapShotAsync(List<RiskSnapshot> snapshots);
        Task<GetOpenTaskResponse>GetOpenTaskItems(int ProjectId);
        Task<Dictionary<long, RiskBaselineDto>> GetRiskSnapshot(long projectId);
        Task SaveRiskSubscriptionAsync(List<RiskSubscription> subscriptions);
        Task<GetRiskSubscriptionResponse> GetRiskSubscriptionAsync(string email);
        Task CancelSubscriptionAsync(List<RiskSubscription> subscription);
    }
}

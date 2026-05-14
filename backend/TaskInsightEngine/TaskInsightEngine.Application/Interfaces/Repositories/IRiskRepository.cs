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
        Task<Dictionary<long, RiskBaselineDto>> GetLatestRiskSnapshot(long projectId);
        Task SaveRiskSubscriptionAsync(List<RiskSubscription> subscriptions);
        Task<List<RiskSubscription>> GetRiskSubscriptionAsync(string email);
        Task CancelSubscriptionAsync(List<RiskSubscription> subscription);
        Task<List<BriefingEntry>> GetProjectBriefingsAsync(List<int> projectIds);
        Task<List<RiskSnapshotDto>> GetRiskSnapshotsAsync(int projectId);
    }
}

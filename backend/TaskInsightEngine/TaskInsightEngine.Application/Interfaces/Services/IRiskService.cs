using TaskInsightEngine.Application.Dtos.Risk;

namespace TaskInsightEngine.Application.Interfaces.Services
{
    public interface IRiskService
    {
        public CalculateRiskResponse RiskCalculator(CalculateRiskRequest request);
        public Task<SaveBriefingDetailsResponse> SaveBriefingDetailsAsync(SaveBriefingDetailsRequest request);
        public Task<CreateProjectRiskSubscriptionResponse> CreateProjectRiskSubcriptionAsync(CreateProjectRiskSubscriptionRequest request);
        public Task<GetProjectRiskSubscriptionResponse> GetProjectRiskSubcriptionAsync(GetProjectRiskSubscriptionRequest request);
        public Task<CancelRiskSubscriptionResponse> CancelProjectRiskSubscriptionAsync(CancelProjectRiskSubscriptionRequest request);
    }
}

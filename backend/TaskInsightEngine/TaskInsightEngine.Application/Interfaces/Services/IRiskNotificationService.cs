using TaskInsightEngine.Application.Dtos.Risk;

namespace TaskInsightEngine.Application.Interfaces.Services
{
    public interface IRiskNotificationService
    {
        Task NotifyBriefingAsync(NotifyRiskDetailsRequest request );
    }
}

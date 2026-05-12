using Microsoft.AspNetCore.SignalR;
using TaskInsightEngine.Api.Hubs;
using TaskInsightEngine.Application.Dtos.Risk;
using TaskInsightEngine.Application.Interfaces.Services;

namespace TaskInsightEngine.Api.Services
{
    public class RiskNotificationService(IHubContext<RiskHub> hubContext) : IRiskNotificationService
    {
        private readonly IHubContext<RiskHub> _hubContext = hubContext;

        public async Task NotifyBriefingAsync(NotifyRiskDetailsRequest request)
        {
            await _hubContext.Clients.User(request.Email).SendAsync("ReceiveDailyBriefing", request.BriefingDetails, request.ProjectName);
        }
    }
}

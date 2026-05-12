using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TaskInsightEngine.Api.Hubs
{
    [Authorize]
    public class RiskHub : Hub
    {

    }
}
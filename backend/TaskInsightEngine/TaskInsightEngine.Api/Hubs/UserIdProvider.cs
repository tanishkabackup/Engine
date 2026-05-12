using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace TaskInsightEngine.Api.Hubs
{
    public class UserIdProvider : IUserIdProvider
    {
        private readonly ILogger<IUserIdProvider> _logger;

        public UserIdProvider(ILogger<IUserIdProvider> logger)
        {
            _logger = logger;
        }
        public string? GetUserId(HubConnectionContext connection)
        {
            try
            {
                var email = connection.User?.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    _logger.LogWarning("SignalR connection attempted but Email claim was not found for ConnectionId: {ConnectionId}", connection.ConnectionId);
                    return null;
                }

                return email;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while resolving User ID for SignalR connection {ConnectionId}", connection.ConnectionId);
                return null;
            }
        }
    }
}

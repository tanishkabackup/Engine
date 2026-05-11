using Hangfire.Dashboard;
using System.Security.Claims;
using TaskInsightEngine.Domain.Enums;

namespace TaskInsightEngine.Infrastructure.Services
{
    public class HangfireAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var user = context.GetHttpContext().User;

            if (user.Identity?.IsAuthenticated is true)
            {
                var roleId = user.FindFirst(ClaimTypes.Role)?.Value;

                string roleName = RoleTypes.MapIdToName(roleId);

                return roleName == RoleTypes.ProjectManager || roleName == RoleTypes.Developer;
            }
            return false;
        }
    }
}
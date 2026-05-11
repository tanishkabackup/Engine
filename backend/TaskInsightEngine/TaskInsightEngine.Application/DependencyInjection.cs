using Microsoft.Extensions.DependencyInjection;
using TaskInsightEngine.Application.Interfaces.Services;
using TaskInsightEngine.Application.Services;

namespace TaskInsightEngine.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services )
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IRiskService, RiskService>();
            return services;
        }
    }
}

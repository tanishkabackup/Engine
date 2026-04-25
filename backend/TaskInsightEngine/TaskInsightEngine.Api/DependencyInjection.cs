using TaskInsightEngine.Application;
using TaskInsightEngine.Application.Dtos.Auth;
using TaskInsightEngine.Infrastructure;

namespace TaskInsightEngine.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationDI(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddInfrastructureDI(configuration);
            services.AddApplicationDI();
            return services;
        }

    }
}

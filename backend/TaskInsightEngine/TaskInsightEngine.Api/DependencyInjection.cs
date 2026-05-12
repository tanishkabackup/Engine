using FluentValidation;
using Microsoft.AspNetCore.SignalR;
using TaskInsightEngine.Api.Hubs;
using TaskInsightEngine.Api.Services;
using TaskInsightEngine.Application;
using TaskInsightEngine.Application.Dtos.Risk;
using TaskInsightEngine.Application.Interfaces.Services;
using TaskInsightEngine.Infrastructure;

namespace TaskInsightEngine.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationDI(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IRiskNotificationService, RiskNotificationService>();
            services.AddSingleton<IUserIdProvider, UserIdProvider>();
            services.AddValidatorsFromAssembly(typeof(ScheduleRiskDeliveryRequest).Assembly);
            services.AddInfrastructureDI(configuration);
            services.AddApplicationDI();
            return services;
        }

    }
}

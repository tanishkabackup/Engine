using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskInsightEngine.Infrastructure.Services;

namespace TaskInsightEngine.Infrastructure.Persistence.Configurations
{
    public static class HangfireSetup
    {
        public static IServiceCollection HangfireInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHangfire((provider, configuration) =>
            {

                var dbSettings = provider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                                  .UseSimpleAssemblyNameTypeSerializer()
                                  .UseRecommendedSerializerSettings()
                                  .UsePostgreSqlStorage(
                                   options =>
                                   {
                                       options.UseNpgsqlConnection(dbSettings.DefaultConnection);
                                       new PostgreSqlStorageOptions
                                       {
                                           QueuePollInterval = TimeSpan.FromSeconds(15),
                                           InvisibilityTimeout = TimeSpan.FromMinutes(30),
                                           JobExpirationCheckInterval = TimeSpan.FromHours(1),
                                           DistributedLockTimeout = TimeSpan.FromMinutes(1),
                                           PrepareSchemaIfNecessary = true,
                                           SchemaName = "hangfire"
                                       };
                                   });


            });

            services.AddHangfireServer(options =>
            {
                options.WorkerCount = Environment.ProcessorCount * 2;
                options.Queues = ["critical", "default"];

            });

            return services;
        }


        public static IApplicationBuilder UseHangFireDashboard(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = [new HangfireAuthFilter()],
                DashboardTitle = "Risk System Jobs",

            });

            return app;
        }
    }
}

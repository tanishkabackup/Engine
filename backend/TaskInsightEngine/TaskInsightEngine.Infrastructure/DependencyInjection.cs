using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using TaskInsightEngine.Application.Dtos.Auth;
using TaskInsightEngine.Application.Dtos.Risk;
using TaskInsightEngine.Application.Interfaces.Cache;
using TaskInsightEngine.Application.Interfaces.Repositories;
using TaskInsightEngine.Application.Interfaces.Services;
using TaskInsightEngine.Infrastructure.Caching;
using TaskInsightEngine.Infrastructure.Jobs;
using TaskInsightEngine.Infrastructure.Persistence;
using TaskInsightEngine.Infrastructure.Persistence.Configurations;
using TaskInsightEngine.Infrastructure.Repositories;
using TaskInsightEngine.Infrastructure.Services;

namespace TaskInsightEngine.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var dbSettings = provider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                options.UseNpgsql(dbSettings.DefaultConnection);
            });

            var cacheSettings = configuration.GetSection(RedisSettings.Section).Get<RedisSettings>();
            var multiplexer = ConnectionMultiplexer.Connect(cacheSettings.ConnectionStrings);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPriorityRepository, PriorityRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICacheService, RedisService>();
            services.AddScoped<IRiskRepository, RiskRepository>();
            services.AddScoped<IJobScheduler,HangfireJobScheduler>();
            services.Configure<AuthCookieSettings>(configuration.GetSection(AuthCookieSettings.Section));
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));
            services.Configure<RedisSettings>(configuration.GetSection(RedisSettings.Section));
            services.Configure<RiskEngineSettings>(configuration.GetSection(RiskEngineSettings.Section));
            services.HangfireInfrastructure(configuration);
            return services;
        }
    }
}

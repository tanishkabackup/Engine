using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskInsightEngine.Api.Extensions;
using TaskInsightEngine.Api.Hubs;
using TaskInsightEngine.Infrastructure.Persistence;
using TaskInsightEngine.Infrastructure.Persistence.Configurations;

namespace TaskInsightEngine.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logSettings = builder.Configuration.GetSection(LogSettings.Section).Get<LogSettings>()!;
            var signalRsettings = builder.Configuration.GetSection(HubSettings.Section).Get<HubSettings>()!;
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {level:u3} {Message:lj} {NewLine} {Exception}]")
                .WriteTo.File(logSettings.LogFilePath , rollingInterval: RollingInterval.Day)
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            // Add services to the container.
            builder.Services.AddOptions<DatabaseSettings>().Bind(builder.Configuration.GetSection(DatabaseSettings.Section)).ValidateOnStart();


            builder.Host.UseSerilog();

            builder.Services.AddOptions<DatabaseSettings>()
                .Bind(builder.Configuration.GetSection(DatabaseSettings.Section))
                .ValidateOnStart();

            builder.Services.AddPresentationDI(builder.Configuration);
            builder.Services.AddSignalR();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Frontend", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            builder.Services.AddAuthentication(builder.Configuration);
            builder.Services.AddSwaggerDocumentation();



            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }

            
            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseRouting();

            app.UseCors("Frontend");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangFireDashboard();
            app.MapControllers();

            app.MapHub<ImpedimentHub>(signalRsettings.ImpedimentHub)
                .RequireCors("Frontend");

            app.MapHub<RiskHub>(signalRsettings.RiskHub)
                .RequireCors("Frontend");

            app.Run();
        }
    }
}
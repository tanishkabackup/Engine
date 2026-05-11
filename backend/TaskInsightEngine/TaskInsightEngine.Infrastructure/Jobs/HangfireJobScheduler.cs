using Hangfire;
using TaskInsightEngine.Application.Dtos.Risk;
using TaskInsightEngine.Application.Interfaces.Services;

namespace TaskInsightEngine.Infrastructure.Jobs
{
    public class HangfireJobScheduler: IJobScheduler
    {
        private readonly IRecurringJobManager _recurringJobManager;

        public HangfireJobScheduler(IRecurringJobManager recurringJobManager)
        {
            _recurringJobManager = recurringJobManager;
        }

        public ScheduleRiskDeliveryResponse ScheduleRiskSubscriptionDelivery(ScheduleRiskDeliveryRequest request)
        {
            var jobId = $"daily-risk-snapshot:{request.Email}";
            var cron = Cron.Daily(request.Hours, request.Minutes);

            var indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

            _recurringJobManager.AddOrUpdate<DailyRiskJob>(
                recurringJobId: jobId,
                methodCall: job => job.RunAsync(request.Email,CancellationToken.None),
                cronExpression: cron, 
                options: new RecurringJobOptions
                {
                  TimeZone = indiaTimeZone
                }
                );

            return new ScheduleRiskDeliveryResponse
            {
                JobId = jobId,
                CronExpression = cron,
                Schedule = $"Daily at {request.Hours:D2}:{request.Minutes:D2} IST"
            };
        }
    }
}

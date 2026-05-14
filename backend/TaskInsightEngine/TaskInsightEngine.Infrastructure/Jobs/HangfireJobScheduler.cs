using Hangfire;
using TaskInsightEngine.Application.Dtos.Risk;
using TaskInsightEngine.Application.Interfaces.Services;
using TaskInsightEngine.Domain.Constants;
using TaskInsightEngine.Domain.Entities;

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
            var jobId = CacheKeys.DailyRiskJobId(request.SubscriptionGuid);
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

        public void DeleteRiskSubscription(List<RiskSubscription> subscriptions)
        {
            subscriptions.ForEach(subscription =>
            {
                var jobId = CacheKeys.DailyRiskJobId(subscription.RiskSubscriptionGuid);

                _recurringJobManager.RemoveIfExists(jobId);
            });
        }
    }
}

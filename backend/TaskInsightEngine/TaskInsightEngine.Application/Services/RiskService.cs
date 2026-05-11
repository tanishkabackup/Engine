using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using TaskInsightEngine.Application.Dtos.Risk;
using TaskInsightEngine.Application.Interfaces.Repositories;
using TaskInsightEngine.Application.Interfaces.Services;
using TaskInsightEngine.Domain.Constants;
using TaskInsightEngine.Domain.Entities;


namespace TaskInsightEngine.Application.Services
{
    public class RiskService : IRiskService
    {
        private readonly RiskEngineSettings _riskEngineSettings;
        private readonly IRiskRepository _riskRepository;
        private readonly IJobScheduler _jobScheduler;
        private readonly ILogger<RiskService> _logger;

        public RiskService(IOptions<RiskEngineSettings> _settings, IRiskRepository riskRepository, IJobScheduler jobScheduler, ILogger<RiskService> logger)
        {
            _riskEngineSettings = _settings.Value;
            _riskRepository = riskRepository;
            _jobScheduler = jobScheduler;
            _logger = logger;
        }

        public CalculateRiskResponse RiskCalculator(CalculateRiskRequest request)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(RiskCalculator));

            try
            {
                var response = new CalculateRiskResponse();

                var weights = _riskEngineSettings.Weights;
                var thresholds = _riskEngineSettings.Thresholds;

                // overdue 

                response.IsOverdue = request.Task.DueDate.Date < request.Today;

                if (response.IsOverdue)
                {
                    var overdueDays = (request.Today - request.Task.DueDate.Date).Days;

                    response.Score += overdueDays * weights.OverDue;

                    response.Reasons.Add($"Overdue by {overdueDays} day(s)");
                }

                // blocker 

                var blockers = request.Task.TaskImpediments?.Count(x => !x.IsResolved) ?? 0;


                response.HasBlockers = blockers > 0;

                if (response.HasBlockers)
                {
                    response.Score += blockers * weights.BlockerPresent;

                    response.Reasons.Add($"{blockers} blocker(s)");
                }

                // daily update check

                var lastUpdate = request.Task.DailyTaskUpdateStatuses?.MaxBy(x => x.UpdatedDate)?.UpdatedDate;

                var daysSinceUpdate = lastUpdate == null ? 1 : Math.Max(0, (request.Today - lastUpdate.Value.Date).Days);

                response.UpdatedToday = daysSinceUpdate == 0;

                if (daysSinceUpdate >= 1)
                {
                    response.Score += daysSinceUpdate * weights.BlockerNoActivity;

                    response.Reasons.Add($"No update for {daysSinceUpdate} day(s)");
                }


                // healthy task

                response.IsHealthy = !response.IsOverdue && !response.HasBlockers && response.UpdatedToday;

                // risk classifciation
                response.Level = DetermineRiskLevel(response.Score, thresholds);

                _logger.LogInformation(
                    "Risk calculation completed for Task {TaskId}. Score: {Score}, Level: {Level}",
                    request.Task.TaskItemId,
                    response.Score,
                    response.Level);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the {Method} process.", nameof(RiskCalculator));
                throw;
            }
        }

        private static string DetermineRiskLevel(int score,Thresholds thresholds)
        {
            return score switch
            {
                _ when score >= thresholds.Critical
                    => RiskTypes.Critical,

                _ when score >= thresholds.Attention
                    => RiskTypes.High,

                _ when score >= thresholds.Monitor
                    => RiskTypes.Medium,

                _ => RiskTypes.Low
            };
        }

        public async Task<SaveBriefingDetailsResponse> SaveBriefingDetailsAsync(SaveBriefingDetailsRequest request)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(SaveBriefingDetailsAsync));
            try
            {
                var response = new SaveBriefingDetailsResponse();

                var items = request.BriefingItems.Select(s => new BriefingItem
                {
                    TaskId = s.TaskId,
                    Score = s.Score,
                    Delta = s.Delta,
                    Level = s.Level,
                    Movement = s.Movement,
                    TopReasons = s.TopReasons,
                    Title = s.Title,
                    AssigneeId = s.AssigneeId,
                    ProjectId = s.ProjectId

                }).ToList();


                response.BriefingDetails = new BriefingSnapshot
                {
                    NeedsAttention = items.Where(i => i.Movement == RiskMovement.Escalated || i.Level == RiskMovement.Critical)
                                    .OrderByDescending(i => i.Delta).ToList(),

                    SlientRisk = items.Where(i => i.Movement == RiskMovement.Silent).OrderByDescending(i => i.Delta).ToList(),
                    Recovering = items.Where(i => i.Movement == RiskMovement.Improved).OrderByDescending(i => i.Delta).ToList(),
                };

                var briefingEntry = new BriefingEntry
                {
                    CreatedAt = DateTime.UtcNow.Date,
                    BriefingSnapshot = response.BriefingDetails,
                    AttentionCount = response.BriefingDetails.NeedsAttention.Count,
                    SilentCount = response.BriefingDetails.SlientRisk.Count,
                    RecoveringCount = response.BriefingDetails.Recovering.Count
                };

                await _riskRepository.SaveRiskBriefingAsync(briefingEntry);

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the {Method} process.", nameof(SaveBriefingDetailsAsync));
                throw;
            }
        }

        public async Task<CreateRiskSubscriptionResponse> CreateRiskSubcriptionAsync(CreateRiskSubscriptionRequest request)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(CreateRiskSubcriptionAsync));
            try
            {

                var existingSubscriptions = await _riskRepository.GetRiskSubscriptionAsync(request.Email);
                await _riskRepository.CancelSubscriptionAsync(existingSubscriptions.Subscriptions);

                var newSubscriptions = request.ProjectIds.Select(id => new RiskSubscription
                {
                    UserEmail = request.Email,
                    ProjectId = id
                }).ToList();

                await _riskRepository.SaveRiskSubscriptionAsync(newSubscriptions);
                var scheduleRiskDeliveryRequest = new ScheduleRiskDeliveryRequest
                {
                    Email = request.Email,
                    Hours = request.Hours,
                    Minutes = request.Minutes

                };

                var jobResponse = _jobScheduler.ScheduleRiskSubscriptionDelivery(scheduleRiskDeliveryRequest);

                return new CreateRiskSubscriptionResponse
                {
                    JobId = jobResponse.JobId,
                    NextRun = jobResponse.Schedule
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the {Method} process.", nameof(CreateRiskSubcriptionAsync));
                throw;
            }
        }

    }
}

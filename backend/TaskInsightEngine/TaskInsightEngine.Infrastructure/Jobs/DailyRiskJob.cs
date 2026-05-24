using Hangfire;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TaskInsightEngine.Application.Dtos.Risk;
using TaskInsightEngine.Application.Interfaces.Repositories;
using TaskInsightEngine.Application.Interfaces.Services;
using TaskInsightEngine.Domain.Constants;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Jobs
{
    public class DailyRiskJob
    {
        private readonly IRiskService _riskService;
        private readonly IRiskRepository _riskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly RiskEngineSettings _riskEngineSettings;
        private readonly IRiskNotificationService _notificationService;
        private readonly ILogger<DailyRiskJob> _logger;

        public DailyRiskJob(IRiskService riskService, IRiskRepository riskRepository, IProjectRepository projectRepository, IRiskNotificationService notificationService, IOptions<RiskEngineSettings> riskSettings, ILogger<DailyRiskJob> logger)
        {
            _riskService = riskService;
            _riskRepository = riskRepository;
            _projectRepository = projectRepository;
            _notificationService = notificationService;
            _riskEngineSettings = riskSettings.Value;
            _logger = logger;
        }

        [AutomaticRetry(Attempts = 1)]
        public async Task RunAsync(string email, CancellationToken token)
        {

            _logger.LogInformation("Starting Daily Risk Job for: {Email}", email);

            try
            {
                var today = DateTime.UtcNow.Date;
                var subscriptions = await _riskRepository.GetRiskSubscriptionAsync(email);

                if (subscriptions == null || subscriptions.Count == 0)
                {
                    _logger.LogWarning("Job aborted: No active risk subscriptions found for {Email}", email);
                    return;
                }

                var allSnapshots = new List<RiskSnapshot>();

                foreach (var subscription in subscriptions)
                {
                    token.ThrowIfCancellationRequested();
                    var projectId = subscription.ProjectId;

                    var getOpenTaskResponse = await _riskRepository.GetOpenTaskItems(projectId);
                    var getRiskSnapshotResponse = await _riskRepository.GetLatestRiskSnapshot(projectId);

                    

                    var taskResponse = getOpenTaskResponse;
                    var latestRisks = getRiskSnapshotResponse;

                    var openTasks = taskResponse?.OpenTasks ?? [];


                    _logger.LogInformation("Project {ProjectId}: Analyzing {TaskCount} open tasks", projectId, openTasks.Count);

                    foreach (var task in openTasks)
                    {
                        token.ThrowIfCancellationRequested();

                        var calculateRiskRequest = new CalculateRiskRequest
                        {
                            Today = today,
                            Task = new TaskRiskDetail
                            {
                                TaskItemId = task.Id,
                                Title = task.Title,
                                DueDate = task.DueDate,
                                AssigneeId = task.AssigneeId,
                                TaskImpediments = task.TaskImpediments ?? [],
                                DailyTaskUpdateStatuses = task.DailyTaskUpdateStatus?
                                    .Where(x => x != null)
                                    .Cast<DailyTaskUpdateStatus>()
                                    .ToList() ?? []
                            }
                        };

                        CalculateRiskResponse result = _riskService.RiskCalculator(calculateRiskRequest);

                        latestRisks.TryGetValue(task.Id, out var baseline);

                        int currentScore = result.Score;
                        int prevScore = baseline?.CurrentScore ?? 0;
                        int delta = currentScore - prevScore;

                        string movement = DetermineMovement(result, baseline, delta);


                        _logger.LogInformation("Task {TaskId}: Score {Score}, Delta {Delta}, Movement {Movement}",
                            task.Id, currentScore, delta, movement);

                        allSnapshots.Add(new RiskSnapshot
                        {
                            TaskId = task.Id,
                            Title = task.Title,
                            ProjectId = projectId,
                            Date = today,
                            CurrentScore = currentScore,
                            CurrentLevel = result.Level,
                            PrevScore = baseline?.CurrentScore,
                            PrevLevel = baseline?.CurrentLevel,
                            Delta = delta,
                            MovementId = RiskMovement.GetMovementId(movement),
                            TopReasons = result.Reasons?.OfType<string>().Take(3).DefaultIfEmpty(RiskMovement.HealthyMessage)
                                        .Aggregate((a, b) => $"{a}, {b}"),
                            AssigneeId = task.AssigneeId
                        });
                    }
                }

                if (allSnapshots.Count != 0)
                {
                    await _riskRepository.AddRiskSnapShotAsync(allSnapshots);

                    await ProcessBriefingsAndNotifications(email, allSnapshots);
                }

                _logger.LogInformation("Daily Risk Job successfully finished for: {Email}", email);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Daily Risk Job for {Email} was cancelled", email);
                throw;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Critical failure in DailyRiskJob for {Email}", email);
                throw;
            }
        }

        private string DetermineMovement(CalculateRiskResponse result, RiskBaselineDto? baseline, int delta)
        {
            if (baseline is null)
            {
                return result.Score >= _riskEngineSettings.MovementRules.EscalationLimit ? RiskMovement.Escalated : RiskMovement.New;

            }

            if (result.IsHealthy)
            {
                return RiskMovement.Healthy;
            }

            var rules = _riskEngineSettings.MovementRules;

            return delta switch
            {
                _ when delta >= rules.EscalationLimit
                    => RiskMovement.Escalated,

                _ when delta <= rules.ImprovementLimit
                    => RiskMovement.Improved,

                _ when Math.Abs(delta) <
                       rules.StabilityTolerance
                    => RiskMovement.Stable,

                _ => RiskMovement.Silent
            };
        }

        private async Task ProcessBriefingsAndNotifications(string email, List<RiskSnapshot> snapshots)
        {
            var groupedByProject = snapshots.GroupBy(x => x.ProjectId);

            var projectIds = groupedByProject.Select(x => x.Key).ToList();

            var projects = await _projectRepository.GetProjectsAsync(projectIds);

            foreach (var project in groupedByProject)
            {
                var projectInfo = projects.FirstOrDefault(x => x.ProjectId == project.Key);
                var briefingItems = project.Select(s => new BriefingItem
                {
                    TaskId = s.TaskId,
                    Title = s.Title,
                    Movement = RiskMovement.GetMovementName(s.MovementId),
                    Level = s.CurrentLevel,
                    Delta = s.Delta,
                    Score = s.CurrentScore,
                    TopReasons = s.TopReasons,
                    AssigneeId = s.AssigneeId,
                    ProjectId = s.ProjectId
                }).ToList();

                var briefingResponse = await _riskService.SaveBriefingDetailsAsync(new SaveBriefingDetailsRequest
                {
                    BriefingItems = briefingItems
                }).ConfigureAwait(false);

                await _notificationService.NotifyBriefingAsync(new NotifyRiskDetailsRequest
                {
                    Email = email,
                    ProjectName = projectInfo.ProjectName,
                    ProjectId = projectInfo.ProjectId,
                    BriefingDetails = briefingResponse.BriefingDetails
                });

                _logger.LogInformation("Briefing notification sent to {Email} for Project {ProjectId}", email, project.Key);
            }
        }
    }
}
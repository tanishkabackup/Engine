using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskInsightEngine.Application.Dtos.Risk;
using TaskInsightEngine.Application.Interfaces.Repositories;
using TaskInsightEngine.Domain.Entities;
using TaskInsightEngine.Infrastructure.Persistence;

namespace TaskInsightEngine.Infrastructure.Repositories
{
    public class RiskRepository : IRiskRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RiskRepository> _logger;

        public RiskRepository(ApplicationDbContext context, ILogger<RiskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddRiskDetailsAsync(Risk risk)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(AddRiskDetailsAsync));
            try
            {
                _context.Risks.Add(risk);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Database operations for {Method} is completed ", nameof(AddRiskDetailsAsync));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(AddRiskDetailsAsync));
                throw;
            }
        }

        public async Task AddRiskSnapShotAsync(List<RiskSnapshot> snapshots)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(AddRiskSnapShotAsync));
            try
            {
                await _context.RiskSnapshots.AddRangeAsync(snapshots);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(AddRiskSnapShotAsync));
                throw;
            }
        }

        public async Task<GetOpenTaskResponse> GetOpenTaskItems(int ProjectId)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(GetOpenTaskItems));
            try
             {
                var openTasks = await _context.TaskAssignments.Where(t => t.TaskItem.ProjectId == ProjectId).AsNoTracking().Select(t => new OpenTaskDto
                {
                    Id = t.TaskItemId,
                    Title = t.TaskItem.Title,
                    DueDate = t.TaskItem.ExpectedETA,
                    AssigneeId = t.AssigneeId,
                    TaskImpediments = t.TaskItem.TaskImpediments.Where(i => !i.IsResolved).ToList(),
                    DailyTaskUpdateStatus = t.TaskItem.DailyTaskUpdateStatuses.GroupBy(d => d.ProjectMemberId)
                                           .Select(g => g.OrderByDescending(s => s.UpdatedDate).FirstOrDefault()).ToList()

                }).ToListAsync();

                return new GetOpenTaskResponse
                {
                    OpenTasks = openTasks
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(GetOpenTaskItems));
                throw;
            }
        }

        public async Task<Dictionary<long, RiskBaselineDto>> GetRiskSnapshot(long projectId)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(GetRiskSnapshot));
            try
            {
                return await _context.Database
                    .SqlQuery<RiskBaselineDto>(
                        $@"SELECT * FROM get_latest_risk({projectId})")
                    .ToDictionaryAsync(x => x.TaskId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(GetRiskSnapshot));
                throw;
            }
        }

        public async Task SaveRiskBriefingAsync(BriefingEntry entry)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(SaveRiskBriefingAsync));
            try
            {
                _context.BriefingEntries.Add(entry);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(SaveRiskBriefingAsync));
                throw;
            }
        }

        public async Task SaveRiskSubscriptionAsync(List<RiskSubscription> subscriptions)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(SaveRiskSubscriptionAsync));
            try
            {
                await _context.RiskSubscriptions.AddRangeAsync(subscriptions);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(SaveRiskSubscriptionAsync));
                throw;
            }
        }

        public async Task<GetRiskSubscriptionResponse> GetRiskSubscriptionAsync(string email)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(GetRiskSubscriptionAsync));
            try
            {
                var subscriptions = await _context.RiskSubscriptions.Where(s => s.UserEmail == email).AsNoTracking().ToListAsync();
                return new GetRiskSubscriptionResponse
                {
                    Subscriptions = subscriptions
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(GetRiskSubscriptionAsync));
                throw;
            }
        }

        public async Task CancelSubscriptionAsync(List<RiskSubscription> subscription)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(CancelSubscriptionAsync));
            try
            {
               _context.RiskSubscriptions.RemoveRange(subscription);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(CancelSubscriptionAsync));
                throw;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskInsightEngine.Application.Dtos.Task;
using TaskInsightEngine.Application.Interfaces.Repositories;
using TaskInsightEngine.Domain.Entities;
using TaskInsightEngine.Infrastructure.Persistence;
using TaskAssignment = TaskInsightEngine.Domain.Entities.TaskAssignment;

namespace TaskInsightEngine.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ILogger<TaskRepository> _logger;
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context, ILogger<TaskRepository> logger)
        {
            _logger = logger;
            _context = context;
        }


        public async Task AssignTaskAsync(List<TaskAssignment> assignments)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(AssignTaskAsync));
            try
            {
                
                _context.TaskAssignments.AddRange(assignments);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Database operations for {Method} is completed ", nameof(AssignTaskAsync));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(AssignTaskAsync));
                throw;
            }
        }

        public async Task CreateNewTaskAsync(TaskItem task)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(CreateNewTaskAsync));
            try
            {
                _context.TaskItems.Add(task);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Database operations for {Method} is completed ", nameof(CreateNewTaskAsync));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method} - {@taskTitle}", nameof(CreateNewTaskAsync), task.Title);
                throw;
            }
        }

        public async Task DailyTaskUpdateAsync(DailyTaskUpdateStatus status)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(DailyTaskUpdateAsync));
            try
            {
                _context.DailyTaskUpdateStatuses.Add(status);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Database operations for {Method} is completed ", nameof(DailyTaskUpdateAsync));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(DailyTaskUpdateAsync));
                throw;
            }
        }

        public async Task<List<DailyTaskUpdateStatus>> GetDailyTaskUpdatesAsync(GetDailyTaskUpdatesRequest request)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(GetDailyTaskUpdatesAsync));
            try
            {
                return await _context.DailyTaskUpdateStatuses.AsNoTracking().Include(pm=>pm.ProjectMember).ThenInclude(u=>u.User).Where(dtu => dtu.TaskId == request.TaskId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(GetDailyTaskUpdatesAsync));
                throw;
            }
        }

        public async Task AddTaskImpedimentAsync(TaskImpediment taskImpediment)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(AddTaskImpedimentAsync));
            try
            {
                _context.Add(taskImpediment);
                await _context.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(AddTaskImpedimentAsync));
                throw;
            }
        }

        public async Task<List<TaskAssignment>> GetTaskAssignmentsAsync(int taskItemId)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(GetTaskAssignmentsAsync));
            try
            {
                var taskAssigments = await _context.TaskAssignments
               .Include(p => p.AssigneeMember).ThenInclude(m => m.User)
               .Include(p => p.AssignerMember).ThenInclude(m => m.User)
               .Include(p => p.Manager).ThenInclude(m => m.User)
               .Where(p => p.TaskItemId == taskItemId)
               .AsNoTracking()
               .ToListAsync();

                return taskAssigments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(GetTaskAssignmentsAsync));
                throw;
            }
        }

        public async Task<List<TaskImpediment>> GetTaskImpedimentsAsync(int taskItemId)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(GetTaskImpedimentsAsync));
            try
            {
               return await _context.TaskImpediments.Include(r=>r.Risk).Where(t => t.TaskItemId == taskItemId).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(GetTaskImpedimentsAsync));
                throw;
            }
        }

        public async Task AddImpedimentCommentAsync(TaskImpedimentComment comment)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(AddImpedimentCommentAsync));
            try
            {
                _context.TaskImpedimentComments.Add(comment);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(AddImpedimentCommentAsync));
                throw;
            }
        }
    }
}

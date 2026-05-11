using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pipelines.Sockets.Unofficial.Arenas;
using TaskInsightEngine.Application.Dtos.Project;
using TaskInsightEngine.Application.Dtos.Task;
using TaskInsightEngine.Application.Interfaces.Repositories;
using TaskInsightEngine.Domain.Entities;
using TaskInsightEngine.Domain.Enums;
using TaskInsightEngine.Infrastructure.Persistence;

namespace TaskInsightEngine.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProjectRepository> _logger;

        public ProjectRepository(ApplicationDbContext context, ILogger<ProjectRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddNewProjectAsync(Project project)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(AddNewProjectAsync));
            try
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Database operations for {Method} is completed ", nameof(AddNewProjectAsync));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method} - {@projectName}", nameof(AddNewProjectAsync), project.Name);
                throw;
            }
        }

        public async Task AddProjectMembersAsync(List<ProjectMember> projectMembers)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(AddProjectMembersAsync));
            try
            {
                _context.ProjectMembers.AddRange(projectMembers);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Database operations for {Method} is completed ", nameof(AddProjectMembersAsync));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(AddProjectMembersAsync));
                throw;
            }
        }


        public async Task<GetProjectsResponse> GetProjectDetailsAsync(GetProjectsRequest project)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(GetProjectDetailsAsync));
            try
            {
                var query = _context.Projects.AsNoTracking();
                if (project.ProjectIds.Any() && !project.ProjectIds.Contains(0))
                {
                    query = query.Where(p => project.ProjectIds.Contains(p.ProjectId));
                }
                return new GetProjectsResponse
                {
                    Projects = await query
                              .OrderByDescending(p => p.CreatedAt)
                              .Select(p => new ProjectDetail
                              {
                                  ProjectId = p.ProjectId,
                                  Name = p.Name,
                                  Description = p.Description,
                                  StartDate = p.StartDate,
                                  ClosingDate = p.ClosingDate,
                                  UpdatedAt = p.UpdatedAt,
                                  Priority = PriorityTypes.MapPriority(p.PriorityId),
                                  CreatedAt = p.CreatedAt

                              }).ToListAsync()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(GetProjectDetailsAsync));
                throw;
            }
        }

        public async Task<List<ProjectMember>> GetProjectMembersAsync(GetProjectMembersRequest request)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(GetProjectMembersAsync));
            try
            {
                var query = _context.ProjectMembers.Include(u => u.User).ThenInclude(u => u.Role).AsNoTracking();

                var result = await query.Where(u => u.User.Email == request.Email).ToListAsync();
                var projectIds = result.Select(p => p.ProjectId).ToList();

                var projectsList = await query.Where(p => projectIds.Contains(p.ProjectId)).ToListAsync();

                return projectsList;


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(GetProjectMembersAsync));
                throw;
            }
        }

        public async Task<List<TaskDetailDto>> GetProjectTasksAsync(GetProjectTasksRequest request)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(GetProjectTasksAsync));
            try
            {
                var tasks = await _context.TaskItems
                .Include(t => t.TaskAssignment).ThenInclude(p => p.AssigneeMember).ThenInclude(m => m.User)
                .Include(t => t.TaskAssignment).ThenInclude(p => p.AssignerMember).ThenInclude(m => m.User)
                .Include(t => t.TaskAssignment).ThenInclude(p => p.Manager).ThenInclude(m => m.User)
                .Where(p => p.ProjectId == request.ProjectId)
                .AsNoTracking()
                .ToListAsync();

                var taskList = tasks.Select(t => new TaskDetailDto
                {
                    TaskId = t.TaskItemId,
                    Description = t.Description,
                    PriorityId = t.PriorityId,
                    Hours = t.Hours,
                    Title = t.Title,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    ExpectedEta = t.ExpectedETA,
                    ProjectId = t.ProjectId,
                    Assignees = [.. t.TaskAssignment.Select(a => a.AssigneeMember.User.FullName)],
                    Assigners = [.. t.TaskAssignment.Select(a=>a.AssignerMember.User.FullName)],
                    Managers = [.. t.TaskAssignment.Select(a=>a.Manager.User.FullName)],


                }).ToList();

                return taskList;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(GetProjectTasksAsync));
                throw;
            }
        }

        public async Task<ProjectMember> GetProjectMemberByEmailAsync(string userEmail)
        {
            return await _context.ProjectMembers
                .Include(pm => pm.User)
                .ThenInclude(u => u.Role)
                .AsNoTracking()
                .FirstAsync(u => u.User.Email == userEmail);
        }
    }
}

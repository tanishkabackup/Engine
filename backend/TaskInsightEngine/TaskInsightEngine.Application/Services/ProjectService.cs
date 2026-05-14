using Microsoft.Extensions.Logging;
using TaskInsightEngine.Application.Dtos.Member;
using TaskInsightEngine.Application.Dtos.Project;
using TaskInsightEngine.Application.Dtos.Risk;
using TaskInsightEngine.Application.Dtos.Task;
using TaskInsightEngine.Application.Dtos.Type;
using TaskInsightEngine.Application.Interfaces.Repositories;
using TaskInsightEngine.Application.Interfaces.Services;
using TaskInsightEngine.Domain.Constants;
using TaskInsightEngine.Domain.Entities;
using TaskInsightEngine.Domain.Enums;

namespace TaskInsightEngine.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPriorityRepository _priorityRepository;
        private readonly IRiskRepository _riskRepository;
        private readonly ILogger<ProjectService> _logger;
        public ProjectService(IProjectRepository projectRepository, IPriorityRepository priorityRepository, IRiskRepository riskRepository, ILogger<ProjectService> logger)
        {
            _projectRepository = projectRepository;
            _priorityRepository = priorityRepository;
            _riskRepository = riskRepository;
            _logger = logger;
        }

        public async Task<AddProjectMemberResponse> AddProjectMemberAsync(AddProjectMemberRequest request)
        {
            _logger.LogInformation("The process for {Method} has started - {ProjectName}", nameof(AddProjectMemberAsync), request.ProjectId);
            try
            {


                var projectMembers = request.UserIds.Select(userId => new ProjectMember
                {
                    ProjectId = request.ProjectId,
                    UserId = userId,
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt
                }).ToList();

                await _projectRepository.AddProjectMembersAsync(projectMembers);

                return new AddProjectMemberResponse
                {
                    IsSuccess = true,
                    Message = "Members added to the project successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the {Method} process.", nameof(AddProjectMemberAsync));
                throw;
            }
        }

        public async Task<CreateProjectResponse> CreateProjectAsync(CreateProjectRequest request)
        {
            _logger.LogInformation("The process for {Method} has started - {ProjectName}", nameof(CreateProjectAsync), request.Name);
            try
            {
                var priority = new GetPriorityTypeDto
                {
                    Type = request.Priority
                };


                var priorityType = await _priorityRepository.GetPriorityTypesAsync(priority);

                if (priorityType is null)
                {
                    return new CreateProjectResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid priority type"
                    };
                }

                var project = new Project
                {
                    Name = request.Name,
                    Description = request.Description,
                    PriorityId = priorityType.PriorityId,
                    StartDate = request.StartDate,
                    ClosingDate = request.CloseDate,
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt

                };
                await _projectRepository.AddNewProjectAsync(project);

                _logger.LogInformation("The process for {Method} is sucessfully completed", nameof(CreateProjectAsync));

                return new CreateProjectResponse
                {
                    ProjectId = project.ProjectId,
                    IsSuccess = true,
                    Message = "Project Created Sucessfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the {Method} process.", nameof(CreateProjectAsync));
                throw;
            }
        }

        public async Task<GetProjectsResponse> GetAllProjectsAsync(GetProjectsRequest request)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(GetAllProjectsAsync));
            try
            {
                return await _projectRepository.GetProjectDetailsAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the {Method} process.", nameof(GetAllProjectsAsync));
                throw;
            }
        }


        public async Task<GetProjectMembersResponse> GetProjectMembersAsync(GetProjectMembersRequest request)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(GetProjectMembersAsync));
            try
            {
                var projectMemberList = await _projectRepository.GetProjectMembersAsync(request);


                return new GetProjectMembersResponse
                {
                    Members = projectMemberList.Select(m => new MemberDetail
                    {
                        FullName = m.User.FullName,
                        ProjectId = m.ProjectId,
                        MemberId = m.ProjectMemberId,
                        Role = m.User.Role.Name,
                        Email = m.User.Email
                    }).ToList()
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the {Method} process.", nameof(GetProjectMembersAsync));
                throw;
            }
        }


        public async Task<GetProjectTasksResponse> GetProjectTasksAsync(GetProjectTasksRequest request)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(GetProjectTasksAsync));
            try
            {

                var response = await _projectRepository.GetProjectTasksAsync(request);

                var taskList = response.OrderByDescending(t=>t.UpdatedAt).Select(t => new TaskDetail
                {
                    TaskId = t.TaskId,
                    Description = t.Description,
                    PriorityStatus = PriorityTypes.MapPriority(t.PriorityId),
                    Hours = t.Hours,
                    Title = t.Title,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    ExpectedEta = t.ExpectedEta,
                    ProjectId = t.ProjectId,
                    AllAssignees = string.Join(FormattingConstants.Delimiter, t.Assignees),
                    AllAssigners = string.Join(FormattingConstants.Delimiter, t.Assigners),
                    AllManagers = string.Join(FormattingConstants.Delimiter, t.Managers)

                }).ToList();

                return new GetProjectTasksResponse
                {
                    TaskList = taskList
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured in the {Method}", nameof(GetProjectTasksAsync));
                throw;
            }
        }

        public async Task<GetProjectDashboardResponse> GetProjectDashboardAsync(GetProjectDashboardRequest request)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(GetProjectDashboardAsync));
            try
            {
                var existingSubscriptions = await _riskRepository.GetRiskSubscriptionAsync(request.Email);

                var projectIds = existingSubscriptions
                    .Select(x => x.ProjectId)
                    .Distinct()
                    .ToList();

                if (projectIds.Count == 0)
                {
                    return new GetProjectDashboardResponse
                    {
                        ProjectMetrics = []
                    };
                }


                var projects = await _projectRepository.GetProjectsAsync(projectIds);

                var allBriefings = await _riskRepository.GetProjectBriefingsAsync(projectIds);

                var snapshotsLookup = new Dictionary<int, List<RiskSnapshotDto>>();

                foreach (var projectId in projectIds)
                {
                    var snapshots = await _riskRepository.GetRiskSnapshotsAsync(projectId);
                    snapshotsLookup[projectId] = snapshots ?? [];
                }

                // get all assignee ids
                var allAssigneeIds = snapshotsLookup.Values
                    .SelectMany(x => x)
                    .Where(x => x.AssigneeId > 0)
                    .Select(x => x.AssigneeId)
                    .Distinct()
                    .ToList();

                // Load all projects members 
                var members = await _projectRepository
                    .GetProjectMemberByIdAsync(projectMemberIds: allAssigneeIds);

                var memberLookup = members.ToDictionary(
                    m => m.ProjectMemberId,
                    m => new MemberDetail
                    {
                        FullName = m.User.FullName,
                        Role = m.User.Role.Name
                    });

                var responseMetrics = new List<ProjectMetricsDto>();

                foreach (var project in projects)
                {
                    var taskSnapshots = snapshotsLookup.GetValueOrDefault(project.ProjectId, []);

                    var entry = allBriefings
                        .Where(x => x.BriefingSnapshot != null)
                        .OrderByDescending(x => x.CreatedAt)
                        .FirstOrDefault(b =>
                            b.BriefingSnapshot.NeedsAttention
                                .Concat(b.BriefingSnapshot.SlientRisk)
                                .Concat(b.BriefingSnapshot.Recovering)
                                .Any(t => t.ProjectId == project.ProjectId));

                    var snapshot = entry?.BriefingSnapshot ?? new BriefingSnapshot();

                    // Remove duplicate tasks
                    snapshot.NeedsAttention = [.. (snapshot.NeedsAttention ?? []).DistinctBy(x => x.TaskId)];

                    snapshot.SlientRisk = [.. (snapshot.SlientRisk ?? []).DistinctBy(x => x.TaskId)];

                    snapshot.Recovering = [.. (snapshot.Recovering ?? []).DistinctBy(x => x.TaskId)];

                    var filteredTasks = FilterHealthyTasks(snapshot, taskSnapshots);

                    var taskHistory = GetProjectTaskHistory(taskSnapshots);

                    var workload = GetTeamMembersWorkload(snapshot, filteredTasks.HealthyTasks, memberLookup);

                    responseMetrics.Add(new ProjectMetricsDto
                    {
                        ProjectId = project.ProjectId,
                        ProjectName = project.ProjectName,

                        Metrics = new ProjectHealthMetrics
                        {
                            TotalTasks = filteredTasks.TotalTaskCount,
                            Critical = snapshot.NeedsAttention.Count,
                            Warning = snapshot.SlientRisk.Count,
                            Recovering = snapshot.Recovering.Count,
                            Healthy = filteredTasks.HealthyTasksCount,
                            HealthPercentage = filteredTasks.HealthyTaskPercentage
                        },

                        Groups = new BriefingSnapshotView
                        {
                            NeedsAttention = snapshot.NeedsAttention,
                            SilentRisk = snapshot.SlientRisk,
                            Recovering = snapshot.Recovering,
                            Healthy = filteredTasks.HealthyTasks,
                            History = taskHistory
                        },

                        Workload = workload
                    });
                }

                return new GetProjectDashboardResponse
                {
                    ProjectMetrics = responseMetrics
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during {Method}", nameof(GetProjectDashboardAsync));
                throw;
            }
        }

        private static FilterHealthyTaskResponse FilterHealthyTasks(BriefingSnapshot briefingSnapshot, List<RiskSnapshotDto> riskSnapshots)
        {
            var atRiskTaskIds = briefingSnapshot.NeedsAttention
                .Concat(briefingSnapshot.SlientRisk)
                .Concat(briefingSnapshot.Recovering)
                .Select(x => x.TaskId)
                .ToHashSet();

            var healthyTasks = riskSnapshots
                .Where(x => x.MovementId == RiskMovement.GetMovementId(RiskMovement.Healthy) &&
                      !atRiskTaskIds.Contains(x.TaskId))
                .GroupBy(x => x.TaskId)
                .Select(g => g
                    .OrderByDescending(x => x.Date)
                    .First())
                .ToList();

            var totalCount = atRiskTaskIds.Count + healthyTasks.Count;

            return new FilterHealthyTaskResponse
            {
                TotalTaskCount = totalCount,

                HealthyTasksCount = healthyTasks.Count,

                HealthyTaskPercentage = totalCount == 0 ? 0 : Math.Round((decimal)healthyTasks.Count * 100 / totalCount, 2),

                HealthyTasks = healthyTasks
            };
        }

        private static List<TeamMemberWorkload> GetTeamMembersWorkload(BriefingSnapshot snapshot, List<RiskSnapshotDto> healthyTasks, Dictionary<int, MemberDetail> memberLookup)
        {
            var allItems = snapshot.NeedsAttention
                .Select(x => new
                {
                    x.TaskId,
                    x.AssigneeId,
                    Type = RiskMovement.Critical
                })
                .Concat(snapshot.SlientRisk.Select(x => new
                {
                    x.TaskId,
                    x.AssigneeId,
                    Type = RiskMovement.Silent
                }))
                .Concat(snapshot.Recovering.Select(x => new
                {
                    x.TaskId,
                    x.AssigneeId,
                    Type = RiskMovement.Improved
                }))
                .Concat(healthyTasks.Select(x => new
                {
                    x.TaskId,
                    x.AssigneeId,
                    Type = RiskMovement.Healthy
                }))
                .DistinctBy(x => new
                {
                    x.TaskId,
                    x.AssigneeId,
                    x.Type
                })
                .ToList();

            return [.. allItems
                .GroupBy(x => x.AssigneeId)
                .Select(g =>
                {
                    memberLookup.TryGetValue(g.Key, out var member);

                    return new TeamMemberWorkload
                    {
                        AssigneeName = member?.FullName,

                        RoleName =  member?.Role,

                        TotalItems = g.Count(),

                        Critical = g.Count(x => x.Type == RiskMovement.Critical),

                        Warning = g.Count(x => x.Type == RiskMovement.Silent),

                        Recovering = g.Count(x => x.Type == RiskMovement.Improved),

                        Healthy = g.Count(x => x.Type == RiskMovement.Healthy)
                    };
                })
                .OrderByDescending(x => x.Critical)
                .ThenByDescending(x => x.Warning)
                .ThenByDescending(x => x.Recovering)];
        }

        private static ProjectTaskHistory GetProjectTaskHistory(List<RiskSnapshotDto> riskSnapshots)
        {
            var taskHistory = riskSnapshots
                .GroupBy(x => x.TaskId)
                .SelectMany(group =>
                {
                    var ordered = group.OrderBy(x => x.Date).ToList();

                    return ordered.Select((curr, index) =>
                    {
                        var prev = index > 0 ? ordered[index - 1] : null;

                        // filter duplicate history
                        if (prev != null && prev.CurrentLevel == curr.CurrentLevel && prev.Delta == curr.Delta)
                        {
                            return null;
                        }

                        return new TaskHistoryDto
                        {
                            TaskId = curr.TaskId,

                            Title = curr.Title,

                            Date = curr.Date,

                            RiskChange = $"{curr.PrevLevel} → {curr.CurrentLevel}",

                            Impact = curr.CurrentLevel ==
                                     RiskMovement.Critical ? RiskMovement.Escalated : curr.CurrentLevel ==
                                     RiskMovement.Silent ? RiskMovement.New : curr.Delta > 0 ?
                                     RiskMovement.Escalated : curr.Delta < 0 ? RiskMovement.Improved :
                                     RiskMovement.Stable,

                            Owner = $"{curr.AssigneeName} ({curr.AssigneeRole})",

                            KeyInsight = curr.TopReasons
                        };
                    });
                })
                .Where(x => x != null)
                .OrderBy(x => x.TaskId)
                .ThenByDescending(x => x.Date)
                .ToList();

            return new ProjectTaskHistory
            {
                TaskHistory = taskHistory
            };
        }
    }
}

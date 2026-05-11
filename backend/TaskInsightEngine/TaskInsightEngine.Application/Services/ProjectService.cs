using Microsoft.Extensions.Logging;
using TaskInsightEngine.Application.Dtos.Member;
using TaskInsightEngine.Application.Dtos.Project;
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
        private readonly ILogger<ProjectService> _logger;
        public ProjectService(IProjectRepository projectRepository, IPriorityRepository priorityRepository, ILogger<ProjectService> logger)
        {
            _projectRepository = projectRepository;
            _priorityRepository = priorityRepository;
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
    }
}

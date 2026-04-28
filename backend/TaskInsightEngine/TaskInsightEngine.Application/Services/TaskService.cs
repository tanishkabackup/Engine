using Microsoft.Extensions.Logging;
using System.Net.Cache;
using TaskInsightEngine.Application.Dtos.Project;
using TaskInsightEngine.Application.Dtos.Task;
using TaskInsightEngine.Application.Dtos.TaskImpediment;
using TaskInsightEngine.Application.Dtos.Type;
using TaskInsightEngine.Application.Interfaces.Repositories;
using TaskInsightEngine.Application.Interfaces.Services;
using TaskInsightEngine.Domain.Constants;
using TaskInsightEngine.Domain.Entities;
using TaskInsightEngine.Domain.Enums;

namespace TaskInsightEngine.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPriorityRepository _priorityReposistory;
        private readonly IRiskRepository _riskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepository, IPriorityRepository priorityRepository, IRiskRepository riskRepository,IProjectRepository projectRepository ,ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _priorityReposistory = priorityRepository;
            _riskRepository = riskRepository;
            _projectRepository = projectRepository;
            _logger = logger;
        }

        public async Task<AssignTaskResponse> AssignTaskAsync(AssignTaskRequest request)
        {
            _logger.LogInformation("The process for {Method} has started ", nameof(AssignTaskAsync));
            try
            {
                var taskAssignments = request.TaskAssignments.Select(t =>
                new TaskAssignment
                {
                    TaskItemId = t.TaskId,
                    AssigneeId = t.AssigneeId,
                    AssignerId = t.AssignerId,
                    ManagerId = t.ManagerId,
                    OpeningDate = t.OpeningDate,
                    ClosingDate = t.ClosingDate
                }).ToList();


                await _taskRepository.AssignTaskAsync(taskAssignments);

                return new AssignTaskResponse
                {
                    IsSuccess = true,
                    Message = "Task are assigned successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured in the {Method}", nameof(AssignTaskAsync));
                throw;
            }
        }

        public async Task<CreateTaskResponse> CreateTaskAsync(CreateTaskRequest request)
        {
            _logger.LogInformation("The process for {Method} has started - {TaskName}", nameof(CreateTaskAsync), request.Title);
            try
            {
                var priority = new GetPriorityTypeDto
                {
                    Type = request.Priority
                };

                var taskPriority = await _priorityReposistory.GetPriorityTypesAsync(priority);

                if (taskPriority is null)
                {
                    return new CreateTaskResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid Priority"
                    };
                }
                var task = new TaskItem
                {
                    ProjectId = request.ProjectId,
                    PriorityId = taskPriority.PriorityId,
                    Title = request.Title,
                    Description = request.Description,
                    ExpectedETA = request.ExpectedEta,
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt,
                    Hours = request.Hours

                };

                await _taskRepository.CreateNewTaskAsync(task);

                return new CreateTaskResponse
                {
                    TaskId = task.TaskItemId,
                    IsSuccess = true,
                    Message = "Task was created successfully"
                };


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured in the {Method}", nameof(CreateTaskAsync));
                throw;
            }
        }

        public async Task<DailyTaskUpdateResponse> DailyTaskUpdateAsync(DailyTaskUpdateRequest request)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(DailyTaskUpdateAsync));
            try
            {
                var statusId = StatusTypes.MapStatus(request.Status);
                var dailyTaskUpdateStatus = new DailyTaskUpdateStatus
                {
                    TaskId = request.TaskId,
                    EffortHours = request.EffortHours,
                    Comment = request.Comment,
                    UpdatedEta = request.UpdatedEta,
                    StatusId = statusId,
                    ProjectMemberId = request.ProjectMemberId
                };

                await _taskRepository.DailyTaskUpdateAsync(dailyTaskUpdateStatus);

                return new DailyTaskUpdateResponse
                {
                    IsSuccess = true,
                    Message = "Daily task update submitted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured in the {Method}", nameof(DailyTaskUpdateAsync));
                throw;
            }
        }

        public async Task<GetDailyTaskUpdateResponse> GetDailyTaskUpdatesAsync(GetDailyTaskUpdatesRequest request)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(GetDailyTaskUpdatesAsync));
            try
            {
                var taskUpdates = await _taskRepository.GetDailyTaskUpdatesAsync(request);

                var response = taskUpdates.Select(dtu => new DailyTaskUpdateDetail
                {
                    Status = StatusTypes.MapStatusTypes(dtu.StatusId),
                    EffortHours = dtu.EffortHours,
                    Comment = dtu.Comment,
                    UpdatedEta = dtu.UpdatedEta,
                    LastUpdatedDate = dtu.UpdatedDate,
                    DailyTaskUpdateStatusId = dtu.DailyTaskUpdateStatusId,
                    ProjectMemberName = dtu.ProjectMember.User.FullName,
                    Email = dtu.ProjectMember.User.Email

                }).ToList();

                return new GetDailyTaskUpdateResponse
                {
                    DailyTaskUpdates = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured in the {Method}", nameof(GetDailyTaskUpdatesAsync));
                throw;
            }
        }

        public async Task<AddTaskImpedimentResponse> AddTaskImpedimentAsync(AddTaskImpedimentRequest request)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(AddTaskImpedimentAsync));
            try
            {
                var createRiskDeatils = new Risk
                {
                    Name = request.Title,
                    Description = request.RiskDescription,
                    Weight = RiskTypes.MapRiskWeights(request.RiskStatus)
                };

                await _riskRepository.AddRiskDetailsAsync(createRiskDeatils);
                var email = request.ResolvedBy;

                DateTime resolvedDate;

                var taskImpediment = new TaskImpediment();


                var taskAssignments = await _taskRepository.GetTaskAssignmentsAsync(request.TaskId);

                if (request.IsResolved is true)
                {
                    var resolvedMemberId = GetMemberId(request.ResolvedBy, taskAssignments);
                    resolvedDate = DateTime.UtcNow;
                    taskImpediment.ResolvedAt = resolvedDate;
                    taskImpediment.ResolvedBy = resolvedMemberId.Value;

                }

                var createdByMemberId = GetMemberId(request.CreatedBy, taskAssignments);


                taskImpediment.Title = request.Title;
                taskImpediment.IsResolved = request.IsResolved;
                taskImpediment.TaskItemId = request.TaskId;
                taskImpediment.RiskId = createRiskDeatils.RiskId;
                taskImpediment.CreatedBy = createdByMemberId.Value;



                await _taskRepository.AddTaskImpedimentAsync(taskImpediment);

                return new AddTaskImpedimentResponse
                {
                    IsSuccess = true,
                    Message = "TaskImpediment Added successfully",
                    TaskImpedimentId = taskImpediment.TaskImpedimentId
                };


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured in the {Method}", nameof(AddTaskImpedimentAsync));
                throw;
            }


        }

        // Get projectmemberId from the resolvedby email
        private static int? GetMemberId(string email, List<TaskAssignment> taskAssignments)
        {
            return taskAssignments
                   .SelectMany(ta => new[]
                  {

                    new { Id = (int?)ta.AssigneeId, Email = ta.AssigneeMember?.User?.Email },
                    new { Id = (int?)ta.AssignerId, Email = ta.AssignerMember?.User?.Email },
                    new { Id = (int?)ta.ManagerId,  Email = ta.Manager?.User?.Email }
                  })
                  .FirstOrDefault(match => match.Id != null &&
                   string.Equals(match.Email, email, StringComparison.OrdinalIgnoreCase))?.Id;

        }

        public async Task<GetTaskImpedimentResponse> GetTaskImpedimentAsync(GetTaskImpedimentRequest request)
        {
            try
            {
                var resposne = await _taskRepository.GetTaskImpedimentsAsync(request.TaskId);

                var taskAssignments = await _taskRepository.GetTaskAssignmentsAsync(request.TaskId);

                var taskImpedimentsResponse = new GetTaskImpedimentResponse
                {
                    TaskImpediments = [.. resposne.OrderByDescending(ti => ti.CreatedAt).Select(ti => new TaskImpedimentDetail
                    {
                        TaskImpedimentId = ti.TaskImpedimentId,
                        Title = ti.Title,
                        RiskDescription = ti.Risk.Description,
                        ResolvedAt = ti.ResolvedAt,
                        ResolvedBy = GetMemberNames(ti.ResolvedBy, taskAssignments),
                        IsResolved = ti.IsResolved,
                        LastUpdated = ti.UpdatedAt,
                        CreatedAt = ti.CreatedAt,
                        CreatedBy = GetMemberNames(ti.CreatedBy, taskAssignments),
                        RiskName = ti.Risk.Name,
                        RiskStatus = RiskTypes.MapRiskStatus(ti.Risk.Weight),

                    })]

                };

                return taskImpedimentsResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error ocurred in the { Method}", nameof(AddTaskImpedimentAsync));
                throw;
            }
        }

        private static string? GetMemberNames(int projectMemberId, List<TaskAssignment> taskAssignments)
        {
            return taskAssignments
                   .SelectMany(ta => new[]
                   {

                    new { Id = (int?)ta.AssigneeId, FullName = ta.AssigneeMember?.User?.FullName },
                    new { Id = (int?)ta.AssignerId, FullName = ta.AssignerMember?.User?.FullName },
                    new { Id = (int?)ta.ManagerId,  FullName = ta.Manager?.User?.FullName }
                   })
                  .FirstOrDefault(ta => ta.Id == projectMemberId)?.FullName;
        }

        public async Task<ImpedimentCommentDto> AddImpedimentCommentAsync(AddImpedimentCommentRequest request)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(AddImpedimentCommentAsync));
            try
            {
                var projectMemberId = await _projectRepository.GetProjectMemberByEmailAsync(request.Email);
                var ImpedimentComment = new TaskImpedimentComment
                {
                    TaskImpedimentId = request.TaskImpedimentId,
                    Comment = request.Comment,
                    CreatedBy = projectMemberId.ProjectMemberId,

                };

                await _taskRepository.AddImpedimentCommentAsync(ImpedimentComment);

                

                return new ImpedimentCommentDto
                {
                    CommentId = ImpedimentComment.TaskImpedimentCommentId ,
                    CreatedAt = ImpedimentComment.CreatedAt,
                    Email = projectMemberId.User.FullName,
                    FullName = request.UserFullName,
                    Message = ImpedimentComment.Comment ,
                };
            }
            catch(Exception ex)
            {
                _logger.LogError("An error ocurred in the {Method}", nameof(AddImpedimentCommentAsync));
                throw;
            }
        }

        public async Task<GetTaskImpedimentCommentsResponse> GetTaskImpedimentCommentsAsync(GetTaskImpedimentCommentsRequest request)
        {
            _logger.LogInformation("The process for {Method} has started ", nameof(GetTaskImpedimentCommentsAsync));
            try
            {
                var response = await _taskRepository.GetTaskImpedimentCommentsAsync(request.TaskImpedimentId);
                var comments = response.Select(comment => new ImpedimentCommentDto
                {
                    CommentId = comment.TaskImpedimentCommentId,
                    FullName = comment.Member.User.FullName,
                    Email = comment.Member.User.Email,
                    Message = comment.Comment,
                    CreatedAt = comment.CreatedAt

                }).ToList();

                return new GetTaskImpedimentCommentsResponse
                {
                    ImpedimentComments = comments
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured in the {Method}", nameof(GetTaskImpedimentCommentsAsync));
                throw;
            }
        }
    }
}

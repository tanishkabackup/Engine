using TaskInsightEngine.Application.Dtos.Task;
using TaskInsightEngine.Application.Dtos.TaskImpediments;

namespace TaskInsightEngine.Application.Interfaces.Services
{
    public interface ITaskService
    {
        Task<CreateTaskResponse> CreateTaskAsync(CreateTaskRequest request);
        Task<AssignTaskResponse> AssignTaskAsync(AssignTaskRequest request);
        Task<DailyTaskUpdateResponse> DailyTaskUpdateAsync(DailyTaskUpdateRequest request);
        Task<GetDailyTaskUpdateResponse> GetDailyTaskUpdatesAsync(GetDailyTaskUpdatesRequest request);
        Task<AddTaskImpedimentResponse> AddTaskImpedimentAsync(AddTaskImpedimentRequest request);
        Task<GetTaskImpedimentResponse> GetTaskImpedimentAsync(GetTaskImpedimentRequest request);
        Task<ImpedimentCommentDto> AddImpedimentCommentAsync(AddImpedimentCommentRequest request);
        Task<GetTaskImpedimentCommentsResponse> GetTaskImpedimentCommentsAsync(GetTaskImpedimentCommentsRequest request);
    }
}

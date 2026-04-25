using TaskInsightEngine.Application.Dtos.Task;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task CreateNewTaskAsync(TaskItem task);
        Task AssignTaskAsync(List<TaskAssignment> assignments);
        Task DailyTaskUpdateAsync(DailyTaskUpdateStatus status);
        Task<List<DailyTaskUpdateStatus>> GetDailyTaskUpdatesAsync(GetDailyTaskUpdatesRequest request);
        Task AddTaskImpedimentAsync(TaskImpediment taskImpediment);
        Task<List<TaskAssignment>> GetTaskAssignmentsAsync(int taskItemId);
        Task<List<TaskImpediment>> GetTaskImpedimentsAsync(int taskItemId);
        Task AddImpedimentCommentAsync(TaskImpedimentComment comment);
    }
}

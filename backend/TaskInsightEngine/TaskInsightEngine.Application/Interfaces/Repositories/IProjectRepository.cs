using TaskInsightEngine.Application.Dtos.Project;
using TaskInsightEngine.Application.Dtos.Task;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task AddNewProjectAsync(Project project);
        Task AddProjectMembersAsync(List<ProjectMember> projectMember);
        Task<ProjectMember> GetProjectMemberByEmailAsync(string email);
        Task<GetProjectsResponse> GetProjectDetailsAsync(GetProjectsRequest project);
        Task<List<ProjectMember>> GetProjectMembersAsync(GetProjectMembersRequest request);
        Task<List<TaskDetailDto>> GetProjectTasksAsync(GetProjectTasksRequest request);
    }
}

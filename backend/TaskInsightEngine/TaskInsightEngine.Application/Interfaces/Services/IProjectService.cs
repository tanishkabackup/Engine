using System.ComponentModel;
using TaskInsightEngine.Application.Dtos.Project;
using TaskInsightEngine.Application.Dtos.Task;

namespace TaskInsightEngine.Application.Interfaces.Services
{
    public interface IProjectService
    {
      Task<CreateProjectResponse> CreateProjectAsync(CreateProjectRequest request);
      Task<AddProjectMemberResponse> AddProjectMemberAsync(AddProjectMemberRequest request);
      Task<GetProjectsResponse> GetAllProjectsAsync(GetProjectsRequest request);
      Task<GetProjectMembersResponse> GetProjectMembersAsync(GetProjectMembersRequest request);
      Task<GetProjectTasksResponse> GetProjectTasksAsync(GetProjectTasksRequest request);
    }
        
}

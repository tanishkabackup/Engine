using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskInsightEngine.Application.Dtos.Project;
using TaskInsightEngine.Application.Interfaces.Services;

namespace TaskInsightEngine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [ApiExplorerSettings(GroupName = "v1")]
        [HttpPost]
        [Authorize]
        [Route("CreateProject")]
        public async Task<IActionResult> CreateProject(CreateProjectRequest request)
        {
            var response = await _projectService.CreateProjectAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("AddProjectMembers")]
        public async Task<IActionResult> AddProjectMembers(AddProjectMemberRequest request)
        {
            var response = await _projectService.AddProjectMemberAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("GetAllProjectMembers")]
        public async Task<IActionResult> GetAllProjectMembers(GetProjectMembersRequest request)
        {
            var response = await _projectService.GetProjectMembersAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("GetAllProjectDetails")]
        public async Task<IActionResult> GetAllProjectDetails(GetProjectsRequest request)
        {
            var response = await _projectService.GetAllProjectsAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("GetProjectTasks")]
        public async Task<IActionResult> GetProjectTasks(GetProjectTasksRequest request)
        {
            var response = await _projectService.GetProjectTasksAsync(request);
            return Ok(response);
        }

    }
}

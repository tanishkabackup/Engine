using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskInsightEngine.Application.Dtos.Project;
using TaskInsightEngine.Application.Dtos.Risk;
using TaskInsightEngine.Application.Interfaces.Services;

namespace TaskInsightEngine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IRiskService _riskService;
        public ProjectController(IProjectService projectService, IRiskService riskService)
        {
            _projectService = projectService;
            _riskService = riskService;
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

        [HttpPost]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
        [ProducesResponseType(typeof(CreateRiskSubscriptionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("CreateRiskSubscription")]
        public async Task<IActionResult> CreateRiskSubscription([FromBody] CreateRiskSubscriptionRequest request,
        [FromServices] IValidator<CreateRiskSubscriptionRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.ToDictionary();
                return ValidationProblem(new ValidationProblemDetails(errors));
            }

            try
            {
                var response = await _riskService.CreateRiskSubcriptionAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while scheduling.");
            }
        }

    }
}

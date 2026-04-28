using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskInsightEngine.Application.Dtos.Task;
using TaskInsightEngine.Application.Dtos.TaskImpediment;
using TaskInsightEngine.Application.Interfaces.Services;

namespace TaskInsightEngine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("CreateTask")]
        public async Task<IActionResult> CreateTask(CreateTaskRequest request)
        {
            var response = await _taskService.CreateTaskAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("AssignTask")]
        public async Task<IActionResult> AssignTask(AssignTaskRequest request)
        {
            var response = await _taskService.AssignTaskAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("DailyTaskUpdate")]
        public async Task<IActionResult> DailyTaskUpdate(DailyTaskUpdateRequest request)
        {
            var response = await _taskService.DailyTaskUpdateAsync(request);
            return Ok(response);
        }


        [HttpPost]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("GetDailyTaskUpdates")]
        public async Task<IActionResult> GetDailyTaskUpdates(GetDailyTaskUpdatesRequest request)
        {
            var response = await _taskService.GetDailyTaskUpdatesAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("AddTaskImpediment")]
        public async Task<IActionResult> AddTaskImpediment(AddTaskImpedimentRequest request)
        {
            var response = await _taskService.AddTaskImpedimentAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("GetTaskImpediments")]
        public async Task<IActionResult> GetTaskImpediments(GetTaskImpedimentRequest request)
        {
            var response = await _taskService.GetTaskImpedimentAsync(request);
            return Ok(response);
        }


        [HttpPost]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("GetTaskImpedimentComments")]
        public async Task<IActionResult> GetTaskImpedimentComments(GetTaskImpedimentCommentsRequest request)
        {
            var response = await _taskService.GetTaskImpedimentCommentsAsync(request);
            return Ok(response);
        }

    }
}

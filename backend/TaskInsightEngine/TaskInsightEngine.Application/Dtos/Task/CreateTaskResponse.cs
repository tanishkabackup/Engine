using TaskInsightEngine.Application.Dtos.Response;

namespace TaskInsightEngine.Application.Dtos.Task
{
    public class CreateTaskResponse: CreateBaseResponse
    {
        public int TaskId { get; set; }
    }
}

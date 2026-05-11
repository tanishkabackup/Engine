using TaskInsightEngine.Application.Dtos.Response;

namespace TaskInsightEngine.Application.Dtos.TaskImpediments
{
    public class AddTaskImpedimentResponse : CreateBaseResponse
    {
        public int TaskImpedimentId { get; set; }
    }
}

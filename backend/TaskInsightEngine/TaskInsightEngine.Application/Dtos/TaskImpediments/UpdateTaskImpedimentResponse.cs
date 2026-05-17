using TaskInsightEngine.Application.Dtos.Response;

namespace TaskInsightEngine.Application.Dtos.TaskImpediments
{
    public class UpdateTaskImpedimentResponse : CreateBaseResponse
    {
        public string? ResolvedBy { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }
}

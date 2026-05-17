namespace TaskInsightEngine.Application.Dtos.TaskImpediments
{
    public class UpdateTaskImpedimentRequest
    {
        public int TaskImpedimentId { get; set; }
        public bool IsResolved { get; set; }
        public string? ResolvedBy { get; set; }
    }
}

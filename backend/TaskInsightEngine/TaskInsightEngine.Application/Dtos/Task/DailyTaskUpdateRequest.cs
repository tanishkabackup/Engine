namespace TaskInsightEngine.Application.Dtos.Task
{
    public class DailyTaskUpdateRequest
    {
        public int TaskId { get; set; }
        public string? Status { get; set; }
        public DateTime UpdatedEta { get; set; }
        public string? Comment { get; set; }
        public int EffortHours { get; set; }
        public int ProjectMemberId { get; set; }
    }
}

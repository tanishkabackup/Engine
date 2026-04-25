namespace TaskInsightEngine.Application.Dtos.Task
{
    public class DailyTaskUpdateDetail
    {
        public int DailyTaskUpdateStatusId { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedEta { get; set; }
        public string Comment { get; set; }
        public int EffortHours { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string ProjectMemberName { get; set; }
        public string Email { get; set; }
    }
}

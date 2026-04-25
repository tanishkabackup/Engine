namespace TaskInsightEngine.Domain.Entities
{
    public class DailyTaskUpdateStatus
    {
        public int DailyTaskUpdateStatusId { get; set; }
        public int TaskId { get; set; }
        public int StatusId { get; set; }
        public int EffortHours { get; set; }
        public string Comment { get; set; }
        public int ProjectMemberId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedEta { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ProjectMember ProjectMember { get; set; }
    }
}
 
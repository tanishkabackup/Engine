namespace TaskInsightEngine.Domain.Entities
{
    public class ProjectMember
    {
        public int ProjectMemberId { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User User { get; set; }
        public ICollection<DailyTaskUpdateStatus> DailyTaskUpdateStatus { get; set; }

        public ICollection<TaskImpedimentComment> TaskImpedimentComments { get; set; }
    }
}

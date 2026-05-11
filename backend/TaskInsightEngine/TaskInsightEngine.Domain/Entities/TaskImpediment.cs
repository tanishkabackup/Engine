namespace TaskInsightEngine.Domain.Entities
{
    public class TaskImpediment
    {
        public int TaskImpedimentId { get; set; }
        public int TaskItemId { get; set; }
        public int RiskId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? ResolvedBy { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public bool IsResolved { get; set; }
        public TaskItem TaskItem { get; set; }

        public ICollection<TaskAssignment> TaskAssignments { get; set; }
        public Risk Risk { get; set; }
        public ICollection<TaskImpedimentComment> TaskImpedimentComments { get; set; }
        
    }
}

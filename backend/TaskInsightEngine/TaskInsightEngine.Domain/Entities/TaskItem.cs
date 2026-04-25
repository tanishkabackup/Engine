namespace TaskInsightEngine.Domain.Entities
{
    public class TaskItem
    {
        public int TaskItemId { get; set; }
        public int ProjectId { get; set; }
        public int PriorityId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Hours { get; set; }
        public DateTime ExpectedETA { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<TaskAssignment> TaskAssignment { get; set; }
        public ICollection<TaskImpediment> TaskImpediments { get; set; }
        
    }
}

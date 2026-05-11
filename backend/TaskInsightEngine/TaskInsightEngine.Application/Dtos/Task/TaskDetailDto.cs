namespace TaskInsightEngine.Application.Dtos.Task
{
    public class TaskDetailDto 
    {
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int PriorityId { get; set; }
        public int Hours { get; set; }
        public DateTime ExpectedEta { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime ClosingDate { get; set; }
        public List<string?> Assignees { get; set; } = [];
        public List<string?> Managers { get; set; } = [];
        public List<string?> Assigners { get; set; } = [];
    }
}

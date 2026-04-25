namespace TaskInsightEngine.Application.Dtos.Task
{
    public class TaskDetail
    {
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PriorityStatus { get; set; }
        public int Hours { get; set; }
        public DateTime ExpectedEta { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<String> Assignees { get; set; }
        public List<String> Managers { get; set; }
        public List<String> Assigners { get; set; }

        public string AllAssignees { get; set; }
        public string AllManagers { get; set; }

        public string AllAssigners { get; set; }
    }
}

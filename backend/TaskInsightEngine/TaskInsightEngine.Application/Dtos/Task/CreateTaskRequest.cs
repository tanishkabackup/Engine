namespace TaskInsightEngine.Application.Dtos.Task
{
    public class CreateTaskRequest
    {
        public int ProjectId { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Hours { get; set; }
        public DateTime ExpectedEta { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

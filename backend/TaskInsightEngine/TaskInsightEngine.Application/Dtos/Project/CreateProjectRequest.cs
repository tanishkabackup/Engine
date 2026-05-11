namespace TaskInsightEngine.Application.Dtos.Project
{
    public class CreateProjectRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

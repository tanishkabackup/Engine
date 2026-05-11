namespace TaskInsightEngine.Application.Dtos.Project
{
    public class ProjectDetail 
    {
        public int ProjectId { get; set; }
        public string? Priority { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

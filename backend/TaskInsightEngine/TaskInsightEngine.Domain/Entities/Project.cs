namespace TaskInsightEngine.Domain.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int PriorityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

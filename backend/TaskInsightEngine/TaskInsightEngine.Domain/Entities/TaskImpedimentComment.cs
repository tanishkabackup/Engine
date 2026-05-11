namespace TaskInsightEngine.Domain.Entities
{
    public class TaskImpedimentComment
    {
        public int TaskImpedimentCommentId { get; set; }
        public int TaskImpedimentId { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public ProjectMember Member { get; set; }
        public TaskImpediment TaskImpediment { get; set; }
    }
}

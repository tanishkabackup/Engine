namespace TaskInsightEngine.Application.Dtos.TaskImpediments
{
    public class ImpedimentCommentDto
    {
        public int CommentId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Message { get; set; }
    }
}

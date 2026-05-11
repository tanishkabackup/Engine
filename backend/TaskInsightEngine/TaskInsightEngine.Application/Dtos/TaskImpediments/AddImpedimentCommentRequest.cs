namespace TaskInsightEngine.Application.Dtos.TaskImpediments
{
    public class AddImpedimentCommentRequest
    {
        public int TaskImpedimentId { get; set; }
        public string? Comment { get; set; }
        public string? UserFullName { get; set; }
        public string Email { get; set; }
    }
}

namespace TaskInsightEngine.Application.Dtos.TaskImpediments
{
    public class SendImpedimentCommentRequest
    {
        public int TaskImpedimentId { get; set; }
        public string? Message { get; set; }
    }
}

namespace TaskInsightEngine.Application.Dtos.TaskImpediment
{
    public class SendImpedimentCommentRequest
    {
        public int TaskImpedimentId { get; set; }
        public string Message { get; set; }
    }
}

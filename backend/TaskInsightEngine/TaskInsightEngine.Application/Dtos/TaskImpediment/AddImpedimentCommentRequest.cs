namespace TaskInsightEngine.Application.Dtos.TaskImpediment
{
    public class AddImpedimentCommentRequest
    {
        public int TaskImpedimentId { get; set; }
        public string Comment { get; set; }
        public string UserFullName { get; set; }
        
    }
}

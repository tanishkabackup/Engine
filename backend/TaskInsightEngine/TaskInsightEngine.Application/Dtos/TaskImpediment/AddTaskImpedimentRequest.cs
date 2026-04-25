namespace TaskInsightEngine.Application.Dtos.TaskImpediment
{
    public class AddTaskImpedimentRequest
    {
        public string Title { get; set; }
        public string RiskStatus { get; set; }
        public bool IsResolved { get; set; }
        public int TaskId { get; set; }
        public string ResolvedBy { get; set; }
        public string CreatedBy { get; set; }
        public string RiskDescription { get; set; }
    }
}

namespace TaskInsightEngine.Application.Dtos.Task
{
    public class AssignTaskDetail
    {
        public int TaskId { get; set; }
        public int AssignerId { get; set; }
        public int AssigneeId { get; set; }
        public int ManagerId { get; set; }
        public DateTime ClosingDate { get; set; }
        public DateTime OpeningDate { get; set; }
    }
}

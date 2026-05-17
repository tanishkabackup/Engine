using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Dtos.TaskImpediments
{
    public class UpdateTaskImpedimentDto
    {
        public TaskImpediment? TaskImpediment { get; set; }
        public List<TaskAssignment> TaskAssignments { get; set; }
        public bool IsSuccess { get; set; }
    }
}

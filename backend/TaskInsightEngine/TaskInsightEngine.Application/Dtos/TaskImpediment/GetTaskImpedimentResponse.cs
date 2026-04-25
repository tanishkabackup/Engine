using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Dtos.TaskImpediment
{
    public class GetTaskImpedimentResponse
    {
       public List<TaskImpedimentDetail> TaskImpediments { get; set; }
    }
}

using TaskInsightEngine.Application.Dtos.TaskImpediment;

namespace TaskInsightEngine.Api.Hubs
{
    public interface ImpedimentClient
    {
        Task ReceiveComment(ImpedimentCommentDto comment);
    }
}

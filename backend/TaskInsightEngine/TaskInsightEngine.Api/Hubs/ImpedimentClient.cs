using TaskInsightEngine.Application.Dtos.TaskImpediments;

namespace TaskInsightEngine.Api.Hubs
{
    public interface ImpedimentClient
    {
        Task ReceiveComment(ImpedimentCommentDto comment);
    }
}

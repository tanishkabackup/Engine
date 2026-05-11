using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TaskInsightEngine.Application.Dtos.TaskImpediments;
using TaskInsightEngine.Application.Interfaces.Services;
using TaskInsightEngine.Domain.Constants;

namespace TaskInsightEngine.Api.Hubs
{
    [Authorize]
    public class ImpedimentHub(ITaskService _service) : Hub<ImpedimentClient>
    {
        public async Task SendComment(SendImpedimentCommentRequest request)
        {
            var userEmail = Context.User.FindFirst(ClaimTypes.Email).Value;
            var saveImpedimentComment = new AddImpedimentCommentRequest
            {
                TaskImpedimentId = request.TaskImpedimentId,
                Comment = request.Message,
                Email = userEmail
            };

            var comment = await _service.AddImpedimentCommentAsync(saveImpedimentComment);


            await Clients.Group(ImpedimentKeys.Group(request.TaskImpedimentId))
                         .ReceiveComment(comment);
        }

        public async Task JoinImpedimentGroup(int impedimentId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, ImpedimentKeys.Group(impedimentId));
        }

       public async Task LeaveImpedimentGroup(int impedimentId)
       {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, ImpedimentKeys.Group(impedimentId));
       }

    }
}

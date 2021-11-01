using Application.Comments;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SignalR
{
    public class ChatHub: Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendComment(Create.Command command) {
            var comment = await _mediator.Send(command);

            await Clients.Group(command.GetTogetherId.ToString())
                .SendAsync("ReceiveComment", comment.Value);
        }

        public override async Task OnConnectedAsync() {
            var httpContext = Context.GetHttpContext();
            var getTogetherId = httpContext.Request.Query["gettogetherId"];
            await Groups.AddToGroupAsync(Context.ConnectionId,getTogetherId);
            var result = await _mediator.Send(new List.Query { GetTogetherId = Guid.Parse(getTogetherId)});
            await Clients.Caller.SendAsync("LoadComments", result.Value);
        }
    }
}

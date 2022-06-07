using Application.Followers;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class FollowController : BaseApiController
    {
        [HttpPost("{username}")]
        public async Task<IActionResult> Follow(string username)
        {
            return HandleResult(await Mediator.Send(new FollowToggle.Command { TargetUsername = username }));
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetFollowings([FromQuery] FollowParams param,CancellationToken token)
        { 
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }, token));
        }
    }
}

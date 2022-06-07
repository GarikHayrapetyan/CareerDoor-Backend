using Application.Profiles;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return HandleResult(await Mediator.Send(new Details.Query { UserName = username }));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(Edit.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpGet("usermeetings")]
        public async Task<IActionResult> GetUserActivities([FromQuery] GetTogetherParams param)
        {
            return HandlePagedResult(await Mediator.Send(new ListActivities.Query { Params = param }));
        }

        [HttpGet("userjobs")]
        public async Task<IActionResult> GetUserJobs(string username, string predicate)
        {
            return HandleResult(await Mediator.Send(new ListJobs.Query { Username = username, Predicate = predicate }));
        }
    }
}

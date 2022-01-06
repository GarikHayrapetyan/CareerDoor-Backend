using Application.Resumes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ResumesController :BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Add.Commmand commmand)
        {
            return HandleResult(await Mediator.Send(commmand));
        }
    }
}

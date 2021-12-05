using Application.Core;
using Application.GetTogethers;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class GetTogetherController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] GetTogetherParams param, CancellationToken token) {

            return HandlePagedResult(await Mediator.Send(new List.Query{Params = param },token));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid id) {

            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(GetTogether getTogether) {

            return HandleResult(await Mediator.Send(new Create.Command { GetTogether=getTogether}));
        }

        [Authorize(Policy = "IsGetTogetherHost")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, GetTogether getTogether) {
            getTogether.Id = id;

            return HandleResult(await Mediator.Send(new Edit.Command { GetTogether=getTogether}));
        }

        [Authorize(Policy = "IsGetTogetherHost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {  
            return HandleResult(await Mediator.Send(new Delete.Command { Id=id}));
        } 

        [HttpPost("{id}/attend")]
        public async Task<IActionResult> Attend(Guid id)
        {
            return HandleResult(await Mediator.Send(new UpdateAttendance.Command { Id = id }));
        }
    }
}

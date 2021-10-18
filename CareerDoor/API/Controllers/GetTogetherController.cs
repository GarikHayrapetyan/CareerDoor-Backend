using Application.GetTogethers;
using Domain;
using MediatR;
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
        public async Task<IActionResult> List(CancellationToken token) {

            return GetTogetherHandleRequest(await Mediator.Send(new List.Query(),token));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid id) {

            return GetTogetherHandleRequest(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(GetTogether getTogether) {

            return GetTogetherHandleRequest(await Mediator.Send(new Create.Command { GetTogether=getTogether}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, GetTogether getTogether) {
            getTogether.Id = id;

            return GetTogetherHandleRequest(await Mediator.Send(new Edit.Command { GetTogether=getTogether}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {  
            return GetTogetherHandleRequest(await Mediator.Send(new Delete.Command { Id=id}));
        } 

        [HttpPost("{id}/attend")]
        public async Task<IActionResult> Attend(Guid id)
        {
            return GetTogetherHandleRequest(await Mediator.Send(new UpdateAttendance.Command { Id = id }));
        }
    }
}

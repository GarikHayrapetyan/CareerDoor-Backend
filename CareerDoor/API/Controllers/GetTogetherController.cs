using Application.GetTogethers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class GetTogetherController : BaseApiController
    {
       
        [HttpGet]
        public async Task<ActionResult<List<GetTogether>>> List() {

            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetTogether>> Details(Guid id) {

            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(GetTogether getTogether) {

            return Ok(await Mediator.Send(new Create.Command { getTogether=getTogether}));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, GetTogether getTogether) {
            getTogether.Id = id;

            return Ok(await Mediator.Send(new Edit.Command { GetTogether=getTogether}));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id,Delete.Command command) {  
            command.Id = id;

            return await Mediator.Send(command);
        }

    }
}

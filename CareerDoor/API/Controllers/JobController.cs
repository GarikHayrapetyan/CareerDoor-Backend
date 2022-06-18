using Application.Jobs;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    //[AllowAnonymous]
    public class JobController : BaseApiController
    {
           
        // GET: api/<JobController>
        [HttpGet]
        public async Task<IActionResult> GetJobs([FromQuery]JobParams param, CancellationToken token)
        {

            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }, token));
        }

        // GET api/<JobController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob (Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id}));
        }

        // POST api/<JobController>
        [HttpPost]
        public async Task<IActionResult> Create(JobDto job)
        {
            return HandleResult(await Mediator.Send(new Create.Command {Job = job}));
        }

        // PUT api/<JobController>/5
        [Authorize(Policy = "IsJobEmployer")]        
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, JobDto job)
        {
            return HandleResult(await Mediator.Send(new Edit.Command {Job = job }));
        }

        // DELETE api/<JobController>/5
        [Authorize(Policy = "IsJobEmployer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id}));
        }

        [HttpPost("{id}/apply")]
        public async Task<IActionResult> Apply(Guid id)
        {
            return HandleResult(await Mediator.Send(new UpdateCandidacy.Command { Id = id }));
        }
    }
}

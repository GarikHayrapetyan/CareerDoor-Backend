using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [AllowAnonymous]
    public class JobController : BaseApiController
    {
        private readonly DataContext _context;

        public JobController(DataContext context)
        {
            _context = context;
        }

    
        // GET: api/<JobController>
        [HttpGet]
        public async Task<ActionResult<List<Job>>> Get()
        {
            return await _context.Jobs.ToListAsync();
        }

        // GET api/<JobController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> Get (Guid id)
        {
            return await _context.Jobs.FindAsync(id);
        }

        // POST api/<JobController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<JobController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<JobController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

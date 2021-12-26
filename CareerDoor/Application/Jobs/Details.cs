using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Jobs
{
    public class Details
    {
        public class Query : IRequest<Job>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Job>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Job> Handle(Query request, CancellationToken cancellationToken)
            {
                var job = await _context.Jobs.FindAsync(request.Id);

                return job;

            }
        }
    }


}

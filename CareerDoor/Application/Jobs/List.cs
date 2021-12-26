using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Jobs
{
    public class List
    {

        public class Query : IRequest<Result<List<Job>>>{}

        public class Handler : IRequestHandler<Query, Result<List<Job>>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<List<Job>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<Job>>.Success(await _context.Jobs.ToListAsync(cancellationToken));
            }
        }
    }
}

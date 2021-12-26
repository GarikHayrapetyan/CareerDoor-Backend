using Application.Core;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Jobs
{
    public class Details
    {
        public class Query : IRequest<Result<Job>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Job>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Job>> Handle(Query request, CancellationToken cancellationToken)
            {
                var job = await _context.Jobs.FindAsync(request.Id);
                return Result<Job>.Success(job);

            }
        }
    }


}

using Application.Core;
using AutoMapper;
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

        public class Query : IRequest<Result<List<JobDto>>>{}

        public class Handler : IRequestHandler<Query, Result<List<JobDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<JobDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var jobs = await _context.Jobs
                    .Include(x => x.Candidates)
                    .ThenInclude(x=>x.AppUser)
                    .ToListAsync(cancellationToken);

                var jobsToReturn = _mapper.Map<List<JobDto>>(jobs);

                return Result<List<JobDto>>.Success(jobsToReturn);
            }
        }
    }
}

using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
                    .ProjectTo<JobDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

              return Result<List<JobDto>>.Success(jobs);
            }
        }
    }
}

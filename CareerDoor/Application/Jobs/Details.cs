using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Jobs
{
    public class Details
    {
        public class Query : IRequest<Result<JobDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<JobDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<JobDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var job = await _context.Jobs
                    .ProjectTo<JobDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x=>x.Id == request.Id);

                return Result<JobDto>.Success(job);

            }
        }
    }


}

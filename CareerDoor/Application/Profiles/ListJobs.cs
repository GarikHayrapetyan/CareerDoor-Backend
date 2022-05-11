using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class ListJobs
    {
        public class Query : IRequest<Result<List<UserJobDto>>>
        {
            public string Username { get; set; }
            public string Predicate { get; set; }
        }


        public class Handler : IRequestHandler<Query, Result<List<UserJobDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<List<UserJobDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.JobCandidate
                .Where(u => u.AppUser.UserName == request.Username)
                .OrderBy(a => a.Job.Date)
                .ProjectTo<UserJobDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

                query = request.Predicate switch
                {
                    "applied" => query.Where(a => a.EmployerUsername !=
                    request.Username),
                    "employer" => query.Where(a => a.EmployerUsername ==
                    request.Username)
                };
                var jobs = await query.ToListAsync();
                return Result<List<UserJobDto>>.Success(jobs);
            }

        }
    }
}

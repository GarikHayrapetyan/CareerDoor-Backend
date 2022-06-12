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
        public class Query : IRequest<Result<PagedList<UserJobDto>>>
        {
            public JobParams Params { get; set; }
        }


        public class Handler : IRequestHandler<Query, Result<PagedList<UserJobDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<PagedList<UserJobDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var param = request.Params;
                var query = _context.JobCandidate
                .Where(u => u.AppUser.UserName == param.Username)
                .OrderBy(a => a.Job.Creation)
                .ProjectTo<UserJobDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

                query = param.Predicate switch
                {
                    "applied" => query.Where(a => a.EmployerUsername !=
                    param.Username),
                    "employer" => query.Where(a => a.EmployerUsername ==
                    param.Username)
                };
 
                return Result<PagedList<UserJobDto>>.Success(
                        await PagedList<UserJobDto>.CreateAsync(query, param.PageNumber,
                        param.PageSize));
            }            
        }
    }
}

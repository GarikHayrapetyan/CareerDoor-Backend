using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Jobs
{
    public class List
    {

        public class Query : IRequest<Result<PagedList<JobDto>>>{
            public JobParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<JobDto>>>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(IUserAccessor userAccessor,DataContext context,IMapper mapper)
            {
                _userAccessor = userAccessor;
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<PagedList<JobDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Jobs
                    .Where(d=>d.Date >=request.Params.ExpirationDate)
                    .OrderBy(d => d.Date)
                    .ProjectTo<JobDto>(_mapper.ConfigurationProvider,new { currentUsername = _userAccessor.GetUsername()})
                    .AsQueryable();

                if (request.Params.IsCandidate && !request.Params.IsEmployer) {
                    query = query.Where(x=>x.Candidates.Any(c=>c.Username == _userAccessor.GetUsername()));
                }

                if (!request.Params.IsCandidate && request.Params.IsEmployer)
                {
                    query = query.Where(x => x.EmployeerUsername==_userAccessor.GetUsername());
                }


                return Result<PagedList<JobDto>>.Success(
                 await PagedList<JobDto>.CreateAsync(query,request.Params.PageNumber,request.Params.PageSize)
                );
            }
        }
    }
}

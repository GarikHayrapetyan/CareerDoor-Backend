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
    public class ListActivities
    {
        public class Query : IRequest<Result<PagedList<UserActivityDto>>>
        {
            public GetTogetherParams Params { get; set; }
        }


        public class Handler : IRequestHandler<Query, Result<PagedList<UserActivityDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<PagedList<UserActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var param = request.Params; 

                var query = _context.GetTogetherAttendees
                .Where(u => u.AppUser.UserName ==param.Username)
                .OrderBy(a => a.GetTogether.Date)
                .ProjectTo<UserActivityDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

                query = param.Predicate switch
                {
                    "past" => query.Where(a => a.Date <= DateTime.Now),
                    "hosting" => query.Where(a => a.HostUsername ==
                    param.Username),
                    _ => query.Where(a => a.Date >= DateTime.Now)
                };
                var activities = await query.ToListAsync();
                return Result<PagedList<UserActivityDto>>.Success(
                        await PagedList<UserActivityDto>.CreateAsync(query, param.PageNumber,
                        param.PageSize));
                //return Result<List<UserActivityDto>>.Success(activities);
            }

        }
    }
}

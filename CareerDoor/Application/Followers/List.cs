using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Followers
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<Profiles.Profile>>>
        {
            public FollowParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<Profiles.Profile>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _context = context;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }

            public async Task<Result<PagedList<Profiles.Profile>>> Handle(Query request, CancellationToken cancellationToken)
            {
                IQueryable<Profiles.Profile> profiles;
                var param = request.Params;

                switch (param.Predicate)
                {
                    case "followers":
                        profiles = _context.UserFollowings.Where(x => x.Target.UserName == param.Username)
                            .Select(u => u.Observer)
                            .ProjectTo<Profiles.Profile>(_mapper.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                            .AsQueryable();
                        return Result<PagedList<Profiles.Profile>>.Success(await PagedList<Profiles.Profile>
                   .CreateAsync(profiles, param.PageNumber, param.PageSize));
                    case "following":
                        profiles = _context.UserFollowings.Where(x => x.Observer.UserName == param.Username)
                            .Select(u => u.Target)
                            .ProjectTo<Profiles.Profile>(_mapper.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                            .AsQueryable();
                        return Result<PagedList<Profiles.Profile>>.Success(await PagedList<Profiles.Profile>
                   .CreateAsync(profiles, param.PageNumber, param.PageSize));
                    default:
                        return Result<PagedList<Profiles.Profile>>.Failure("Non detected predicate.");
                }
            }
        }
    }
}
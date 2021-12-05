using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.GetTogethers
{
    public class List
    {
        public class Query : IRequest<Result<List<GetTogetherDTO>>> { 
        }

        public class Handler : IRequestHandler<Query, Result<List<GetTogetherDTO>>>
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
            public async Task<Result<List<GetTogetherDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var getTogethers = await _context.GetTogethers
                    .ProjectTo<GetTogetherDTO>(_mapper.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .ToListAsync(cancellationToken);

                return Result<List<GetTogetherDTO>>.Success(getTogethers);
            }
        }
    }
}

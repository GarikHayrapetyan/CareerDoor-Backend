using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.GetTogethers
{
    public class Details
    {
        public class Query : IRequest<Result<GetTogetherDTO>> {
            public Guid Id { get; set; }
        }


        public class Handler : IRequestHandler<Query, Result<GetTogetherDTO>>
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
            public async Task<Result<GetTogetherDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var getTogether = await _context.GetTogethers
                   .ProjectTo<GetTogetherDTO>(_mapper.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                   .FirstOrDefaultAsync(x => x.Id == request.Id);

               return Result<GetTogetherDTO>.Success(getTogether);
            }
        }
    }
}

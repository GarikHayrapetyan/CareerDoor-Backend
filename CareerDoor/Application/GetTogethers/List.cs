using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using System;
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
            private readonly ILogger _logger;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<GetTogetherDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var getTogethers = await _context.GetTogethers
                    .Include(a => a.Attendees)
                    .ThenInclude(u => u.AppUser)
                    .ToListAsync(cancellationToken);

                var getTogethersToReturn = _mapper.Map<List<GetTogetherDTO>>(getTogethers);

                return Result<List<GetTogetherDTO>>.Success(getTogethersToReturn);
            }
        }
    }
}

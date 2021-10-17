using Application.Core;
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
        public class Query : IRequest<Result<List<GetTogether>>> { 
        }

        public class Handler : IRequestHandler<Query, Result<List<GetTogether>>>
        {
            private readonly DataContext _context;
            private readonly ILogger _logger;

            public Handler(DataContext context,ILogger<List> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<Result<List<GetTogether>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var getTogethers = await _context.GetTogethers
                    .Include(a => a.Attendees)
                    .ThenInclude(u => u.AppUser)
                    .ToListAsync(cancellationToken);
                return Result<List<GetTogether>>.Success(getTogethers);
            }
        }
    }
}

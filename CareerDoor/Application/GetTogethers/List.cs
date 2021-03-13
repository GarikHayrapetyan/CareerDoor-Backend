using Domain;
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
        public class Query : IRequest<List<GetTogether>> { 
        }

        public class Handler : IRequestHandler<Query, List<GetTogether>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<GetTogether>> Handle(Query request, CancellationToken cancellationToken)
            { 
                return await _context.GetTogethers.ToListAsync();
            }
        }
    }
}

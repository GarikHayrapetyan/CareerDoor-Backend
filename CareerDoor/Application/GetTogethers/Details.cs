using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.GetTogethers
{
    public class Details
    {
        public class Query : IRequest<GetTogether> {
            public Guid Id { get; set; }
        }


        public class Handler : IRequestHandler<Query, GetTogether>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<GetTogether> Handle(Query request, CancellationToken cancellationToken)
            {               

                return await _context.GetTogethers.FindAsync(request.Id);
            }
        }
    }
}

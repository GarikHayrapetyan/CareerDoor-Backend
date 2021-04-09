using Application.Core;
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
        public class Query : IRequest<Result<GetTogether>> {
            public Guid Id { get; set; }
        }


        public class Handler : IRequestHandler<Query, Result<GetTogether>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<GetTogether>> Handle(Query request, CancellationToken cancellationToken)
            {         
               return Result<GetTogether>.Success(await _context.GetTogethers.FindAsync(request.Id));
            }
        }
    }
}

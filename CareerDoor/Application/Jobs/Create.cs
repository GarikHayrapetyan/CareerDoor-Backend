using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Jobs
{
    public class Create
    {
        public class Command : IRequest {
            public Job job { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Jobs.Add(request.job);
                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}

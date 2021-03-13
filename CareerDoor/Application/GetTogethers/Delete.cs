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
    public class Delete
    {
        public class Command : IRequest {
            public Guid Id { get; set; }
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
                var meeting = await _context.GetTogethers.FindAsync(request.Id);

                if (meeting==null)
                {
                    throw new Exception("Could not find meeting");
                }

                _context.GetTogethers.Remove(meeting);
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem during saving changes.");
            }
        }
    }
}

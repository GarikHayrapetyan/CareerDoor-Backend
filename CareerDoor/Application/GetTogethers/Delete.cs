using Application.Core;
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
        public class Command : IRequest<Result<Unit>> {
            public Guid Id { get; set; }
        }


        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var meeting = await _context.GetTogethers.FindAsync(request.Id);

                if (meeting==null)
                {
                   return null;
                }

                _context.GetTogethers.Remove(meeting);
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Result<Unit>.Success(Unit.Value);
                }

                return Result<Unit>.Failure("Failed to delete the meeting.");
            }
        }
    }
}

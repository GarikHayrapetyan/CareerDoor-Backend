using Domain;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.GetTogethers
{
    public class Create
    {
        public class Command : IRequest {
            public GetTogether getTogether { get; set; }
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
                /*var meeting = new GetTogether
                {
                    Title = request.Title,
                    Description = request.Description,
                    Date = request.Date,
                    Link = request.Link,
                    PassCode = request.PassCode,
                };*/

                _context.GetTogethers.Add(request.getTogether);
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

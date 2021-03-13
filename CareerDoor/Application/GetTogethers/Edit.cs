using AutoMapper;
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
    public class Edit
    {
        public class Command :IRequest{
            public GetTogether GetTogether { get; set; }
        }


        public class Handler : IRequestHandler<Command>
        {
            public DataContext _context { get; set; }
            public IMapper _mapper { get; set; }
            public Handler(DataContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var meeting = await _context.GetTogethers.FindAsync(request.GetTogether.Id);

                if (meeting == null)
                {
                    throw new Exception("Could not find meeting.");
                }

                _mapper.Map(request.GetTogether,meeting);

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

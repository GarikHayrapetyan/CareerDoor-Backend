using AutoMapper;
using Domain;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Jobs
{
    public class Edit
    {
        public class Command: IRequest {
            public Job job{ get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.job).SetValidator(new JobValidator());
            }
        }
        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var job = await _context.Jobs.FindAsync(request.job.Id);
                _mapper.Map(request.job,job);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}

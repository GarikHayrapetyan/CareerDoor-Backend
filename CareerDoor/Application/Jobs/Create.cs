using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Jobs
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>> {
            public Job job { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command> {

            public CommandValidator()
            {
                RuleFor(x => x.job).SetValidator(new JobValidator());
            }
        }

        public class Handler : IRequestHandler<Command,Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Jobs.Add(request.job);               

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Result<Unit>.Success(Unit.Value);
                }

                return Result<Unit>.Failure("Failed to save the meeting.");
            }
        }
    }
}

using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x=>x.UserName == _userAccessor.GetUsername());

                var candidate = new JobCandidate
                {
                    AppUser = user,
                    Job = request.job,
                    IsEmployer = true
                };

                request.job.Candidates.Add(candidate);

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

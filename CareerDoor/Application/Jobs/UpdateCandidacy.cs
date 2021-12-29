using Application.Core;
using Application.Interfaces;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Application.Jobs
{
    public class UpdateCandidacy
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
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
                var job = await _context.Jobs
                    .Include(a => a.Candidates).ThenInclude(u => u.AppUser)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);

                if (job == null) return null;

                var user = await _context.Users.FirstOrDefaultAsync(x =>
                    x.UserName == _userAccessor.GetUsername());

                if (user == null) return null;

                var employerUsername = job.Candidates.FirstOrDefault(x => x.IsEmployer)?.AppUser?.UserName;

                var candidancy = job.Candidates.FirstOrDefault(x => x.AppUser.UserName == user.UserName);

                if (candidancy != null && employerUsername == user.UserName)
                    job.IsCanceled= !job.IsCanceled;

                if (candidancy != null && employerUsername != user.UserName)
                    job.Candidates.Remove(candidancy);

                if (candidancy == null)
                {
                    candidancy = new JobCandidate
                    {
                        AppUser = user,
                        Job = job,
                        IsEmployer = false
                    };

                    job.Candidates.Add(candidancy);
                }

                var result = await _context.SaveChangesAsync() > 0;

                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem updating candidancy");
            }

        }
    }
}

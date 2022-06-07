using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Resumes
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IResumeAccessor _resumeAccessor;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IResumeAccessor resumeAccessor, IUserAccessor userAccessor)
            {
                _context = context;
                _resumeAccessor = resumeAccessor;
                _userAccessor = userAccessor;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.Include(p => p.Resumes)
                    .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());


                if (user == null) return null;

                var resume = user.Resumes.FirstOrDefault(x => x.Id == request.Id);

                if (resume == null) return null;


                var result = await _resumeAccessor.DeleteResume(resume.Id);

                if (result == null) return Result<Unit>.Failure("Problem deleting resume from Cloudinary");

                user.Resumes.Remove(resume);
                _context.Resumes.Remove(resume);

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Result<Unit>.Success(Unit.Value);
                }

                return Result<Unit>.Failure("Problem deleting resume from API");
            }
        }
    }
}

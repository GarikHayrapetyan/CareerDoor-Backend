using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Resumes
{
    public class Add
    {
        public class Commmand : IRequest<Result<Resume>>
        {
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Commmand, Result<Resume>>
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
            public async Task<Result<Resume>> Handle(Commmand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.Include(p => p.Resumes)
                     .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                if (user == null)
                {
                    return null;
                }

                var resumeUploadResult = await _resumeAccessor.AddResume(request.File);

                var resume = new Resume
                {
                    Url = resumeUploadResult.Url,
                    Id = resumeUploadResult.PublicId,
                    FileName = resumeUploadResult.FileName
                };

                

                user.Resumes.Add(resume);

                var result = await _context.SaveChangesAsync() > 0;

                if (result) return Result<Resume>.Success(resume);

                return Result<Resume>.Failure("Problem adding resume");
            }
        }
    }
}

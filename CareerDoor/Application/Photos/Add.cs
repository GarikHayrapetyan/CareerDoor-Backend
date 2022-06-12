using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Photos
{
    public class Add
    {
        public class Commmand : IRequest<Result<Photo>> { 
             public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Commmand, Result<Photo>>
        {
            private readonly DataContext _context;
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context,IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
            {
                _context = context;
                _photoAccessor = photoAccessor;
                _userAccessor = userAccessor;
            }   
            public async Task<Result<Photo>> Handle(Commmand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.Include(p => p.Photos)
                     .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                if (user==null) 
                {
                    return null;
                }

                if (user.Photos.Count>=2)
                {
                    return Result<Photo>.Failure("Max count photos is 2.");
                }

                var photoUploadResult = await _photoAccessor.AddPhoto(request.File);

                var photo = new Photo
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId
                };

                if (!user.Photos.Any(x=>x.IsMain))
                {
                    photo.IsMain = true;
                }

                user.Photos.Add(photo);

                var result = await _context.SaveChangesAsync() > 0;

                if (result) return Result<Photo>.Success(photo);

                return Result<Photo>.Failure("Problem adding photo");
            }
        }
    }
}

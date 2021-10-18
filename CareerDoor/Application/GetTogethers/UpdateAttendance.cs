using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.GetTogethers
{
    public class UpdateAttendance
    {
        public class Command: IRequest<Result<Unit>>
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
                var getTogether = await _context.GetTogethers
                    .Include(a => a.Attendees).ThenInclude(u => u.AppUser)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);

                if (getTogether == null) return null;

                var user = await _context.Users.FirstOrDefaultAsync(x =>
                    x.UserName == _userAccessor.GetUsername());

                if (user == null) return null;

                var hostUsername = getTogether.Attendees.FirstOrDefault(x => x.IsHost)?.AppUser?.UserName;

                var attendance = getTogether.Attendees.FirstOrDefault(x => x.AppUser.UserName == user.UserName);

                if (attendance != null && hostUsername == user.UserName)
                    getTogether.IsCancelled = !getTogether.IsCancelled;

                if (attendance != null && hostUsername != user.UserName)
                    getTogether.Attendees.Remove(attendance);

                if (attendance == null)
                {
                    attendance = new GetTogetherAttendee
                    {
                        AppUser = user,
                        GetTogether = getTogether,
                        IsHost = false
                    };

                    getTogether.Attendees.Add(attendance);
                }

                var result = await _context.SaveChangesAsync() > 0;

                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem updating attendance");
            }
        }
    }
}

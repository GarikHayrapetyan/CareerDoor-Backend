using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class ResetPassword
    {

        public class Command : IRequest<Result<Unit>>
        {
            public string Email { get; set; }
            public string OTP { get; set; }
            public string NewPassword { get; set; }
            public UserManager<AppUser> UserManager{ get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.OTP).NotEmpty();
                RuleFor(x => x.NewPassword).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await request.UserManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

                var resetPassword = await _context.ResetPasswords
                    .Where(x => x.UserId == user.Id && x.OTP == request.OTP)
                    .OrderByDescending(x => x.InsertDateTimeUTC)
                    .FirstOrDefaultAsync();

                var otpExpirationTime = resetPassword.InsertDateTimeUTC.AddMinutes(150);

                if (otpExpirationTime < DateTime.UtcNow)
                {
                    return Result<Unit>.Failure("OTP is expired.");
                }

                var result = await request.UserManager.ResetPasswordAsync(user, resetPassword.ResetToken, request.NewPassword);

                if (!result.Succeeded)
                {
                    return Result<Unit>.Failure("Problem reseting the new password.");
                }


                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}

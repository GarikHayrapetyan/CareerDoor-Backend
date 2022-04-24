using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class ResetEmail
    {
        public class Command : IRequest<Result<Unit>> {
            
            public string Email { get; set; }
            public UserManager<AppUser> UserManager { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
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
                var user = await request.UserManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    return Result<Unit>.Failure("Failed to find user by email");
                }

                var resetToken = await request.UserManager.GeneratePasswordResetTokenAsync(user);

                string otp = EmailResetOTP.GenerateOTP();

                var resetPassword = new Domain.ResetPassword
                {
                    Email = request.Email,
                    UserId = user.Id,
                    ResetToken = resetToken,
                    OTP = otp,
                    InsertDateTimeUTC = DateTime.UtcNow
                };

                _context.ResetPasswords.Add(resetPassword);
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    var subject = "CareerDoor One Time Password";
                    var htmlMessage = "Hello "+ request.Email 
                        + $"<br><br>Here is your One Time Password<br><strong>{otp}<strong><br><br><br>CareerDoor.com<br>";

                    await EmailSender.SendEmailAsync(request.Email, subject, htmlMessage);

                    return Result<Unit>.Success(Unit.Value);
                }

                return Result<Unit>.Failure("Problem resetting password");
            }
        }
    }
}

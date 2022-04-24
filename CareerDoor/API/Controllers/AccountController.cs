using API.DTOs;
using API.Services;
using Domain;
using Infrastructure.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
     
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;

        public AccountController(DataContext context,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {

            var user = await _userManager.Users.Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                return CreateUserObject(user);
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                return BadRequest("Email taken");
            }
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            {
                return BadRequest("Username taken");
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
                Country = registerDto.Country,
                City = registerDto.City
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return CreateUserObject(user);
            }

            return BadRequest("Problem registering user");
        }

        [AllowAnonymous]
        [HttpPost("sendResetPassword")]       
        public async Task<IActionResult> SendPasswordResetCode(string email)
        {
            if (string.IsNullOrEmpty(email)) {

                return BadRequest("Email cannot be empty.");
            }

            if (await _userManager.Users.AnyAsync(x=>x.Email==email))
            {
                var user = await _userManager.Users.FirstAsync(x=>x.Email==email);

                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var otp = EmailResetOTP.GenerateOTP();
                var resetPassword = new ResetPassword()
                {
                    UserId = user.Id,
                    OTP = otp,
                    Email = email,
                    ResetToken = resetToken,
                    InsertDateTimeUTC = DateTime.UtcNow
                    
                };

                _context.ResetPasswords.Add(resetPassword);
                await _context.SaveChangesAsync();            

                await EmailSender.SendEmailAsync(email, "CareerDoor One Time Password", "Hello "
                    + email + $"<br><br>Here is your One Time Password<br><strong>{otp}<strong><br><br><br>CareerDoor.com<br>");

                return Ok("Token sent successfully in email");
            }


            return Ok();

            
        }

        [AllowAnonymous]
        [HttpPost("resetPassword")]
        public async Task<IActionResult> PasswordResetCode(string email,string otp, string newPassword) {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(otp) || string.IsNullOrEmpty(newPassword)) {
                return BadRequest("Email, OTP and new password cannot be empty.");            
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.Email==email);

            var resetPassword = await _context.ResetPasswords
                .Where(x => x.UserId == user.Id && x.OTP == otp)
                .OrderByDescending(x => x.InsertDateTimeUTC)
                .FirstOrDefaultAsync();

            var otpExpirationTime = resetPassword.InsertDateTimeUTC.AddMinutes(150);

            if (otpExpirationTime<DateTime.UtcNow) {
                return BadRequest("OTP is expired.");
            }

            var result = await _userManager.ResetPasswordAsync(user,resetPassword.ResetToken,newPassword);

            if (!result.Succeeded) {
                return BadRequest("Problem reseting the new password.");
            }
            

            return Ok("Password is reset successfully.");
;        }





        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.Users.Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));

            return CreateUserObject(user);
        }

        [NonAction]
        public UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Username = user.UserName,
                Image = user?.Photos?.FirstOrDefault(x => x.IsMain)?.Url,
                Token = _tokenService.CreateToken(user),
                Country = user.Country,
                City = user.City                
            };
        }



    }
}

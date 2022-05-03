using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class IsEmployerRequirement: IAuthorizationRequirement
    {
        public class IsEmployerRequirementHandler : AuthorizationHandler<IsEmployerRequirement>
        {
            private readonly DataContext _dbContext;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public IsEmployerRequirementHandler(DataContext dbContext, IHttpContextAccessor httpContextAccessor)
            {
                _dbContext = dbContext;
                _httpContextAccessor = httpContextAccessor;
            }
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsEmployerRequirement requirement)
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null) return Task.CompletedTask;

                var jobId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value?.ToString());

                var candidate = _dbContext.JobCandidate
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.AppUserId == userId && x.JobId == jobId)
                    .Result;

                candidate.AppUserId = userId;

                if (candidate == null) return Task.CompletedTask;

                if (candidate.IsEmployer) context.Succeed(requirement);

                return Task.CompletedTask;
            }
        }
    }
}

using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Jobs
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>> {
            public JobDto Job { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command> {

            public CommandValidator()
            {
                RuleFor(x => x.Job).SetValidator(new JobDtoValidator());
            }
        }

        public class Handler : IRequestHandler<Command,Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
            {
                _context = context;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var jobDto = request.Job;
                jobDto.Creation = DateTime.UtcNow;

                var user = await _context.Users.FirstOrDefaultAsync(x=>x.UserName == _userAccessor.GetUsername());
                var jobType = await _context.JobType.FirstOrDefaultAsync(x => x.Type == jobDto.Type);

                if(jobType == null)
                {
                    return Result<Unit>.Failure("Job type does not exist.");
                }

                var job = new Job();

                _mapper.Map(jobDto, job);

                job.JobTypeId = jobType.Id;

                var candidate = new JobCandidate
                {
                    AppUser = user,
                    Job = job,
                    IsEmployer = true
                };


                job.Candidates.Add(candidate);

                _context.Jobs.Add(job);               

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

using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Jobs
{
    public class Edit
    {
        public class Command: IRequest<Result<Unit>> {
            public JobDto Job{ get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Job).SetValidator(new JobDtoValidator());
            }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
           

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var jobDto = request.Job;
                var job = await _context.Jobs.FindAsync(jobDto.Id);

                if (job == null)
                {
                    return null;
                }

                var jobType = await _context.JobType.FirstOrDefaultAsync(x => x.Type == jobDto.Type);

                if (jobType == null)
                {
                    return Result<Unit>.Failure("Job type does not exist.");
                }

                var jobExperience = await _context.JobExperience.FirstOrDefaultAsync(x => x.Experience == jobDto.Experience);

                if (jobExperience == null)
                {
                    return Result<Unit>.Failure("Job experience does not exist.");
                }

                jobDto.Creation = job.Creation;
                _mapper.Map(jobDto, job);

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Result<Unit>.Success(Unit.Value);
                }

                return Result<Unit>.Failure("Failed to update the job.");
            }
        }
    }
}

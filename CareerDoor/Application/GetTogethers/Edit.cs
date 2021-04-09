using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.GetTogethers
{
    public class Edit
    {
        public class Command :IRequest<Result<Unit>>{
            public GetTogether GetTogether { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command> {
            public CommandValidator()
            {
                RuleFor(x => x.GetTogether).SetValidator(new GetTogetherValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            public DataContext _context { get; set; }
            public IMapper _mapper { get; set; }
            public Handler(DataContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var meeting = await _context.GetTogethers.FindAsync(request.GetTogether.Id);

                if (meeting == null)
                {
                    return null;
                }

                _mapper.Map(request.GetTogether,meeting);

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Result<Unit>.Success(Unit.Value);
                }

                return Result<Unit>.Failure("Failed to update the meeting.");
            }
        }
    }
}

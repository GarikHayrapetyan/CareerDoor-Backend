using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.GetTogethers
{
    public class Details
    {
        public class Query : IRequest<Result<GetTogetherDTO>> {
            public Guid Id { get; set; }
        }


        public class Handler : IRequestHandler<Query, Result<GetTogetherDTO>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<GetTogetherDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var getTogether = await _context.GetTogethers
                   .ProjectTo<GetTogetherDTO>(_mapper.ConfigurationProvider)
                   .FirstOrDefaultAsync(x => x.Id == request.Id);

               return Result<GetTogetherDTO>.Success(getTogether);
            }
        }
    }
}

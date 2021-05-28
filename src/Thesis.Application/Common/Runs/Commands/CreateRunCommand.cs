
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;

namespace Thesis.Application.Common.Runs.Commands
{
    public class CreateRunCommand : IRequest<int>
    {
        public decimal RouteId { get; init; }
        public decimal Latitude { get; init; }
        public decimal Longitude { get; init; }
    }

    public class CreateRunCommandHandler : IRequestHandler<CreateRunCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Run> _repository;
        private readonly ICurrentUserService _currentUserService;

        public CreateRunCommandHandler(IMapper mapper, IRepository<Run> repository, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateRunCommand request, CancellationToken cancellationToken)
        {


            return 2;
        }
    }
}

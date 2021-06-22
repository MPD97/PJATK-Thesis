
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;
using Thesis.Domain.Exceptions;

namespace Thesis.Application.Common.Routes.Commands.CreateRun
{
    public class CreateRunCommand : IRequest<RunDto>
    {
        public int RouteId { get; init; }
        public decimal Latitude { get; init; }
        public decimal Longitude { get; init; }
        public int Accuracy { get; init; }

        public CreateRunCommand()
        {

        }
        public CreateRunCommand(int routeId, decimal latitude, decimal longitude, int accuracy)
        {
            RouteId = routeId;
            Latitude = latitude;
            Longitude = longitude;
            Accuracy = accuracy;
        }
    }

    public class CreateRunCommandHandler : IRequestHandler<CreateRunCommand, RunDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Run> _repository;

        private readonly IRouteService _routeService;
        private readonly IPointService _pointService;
        private readonly IRunService _runService;

        private readonly ICurrentUserService _currentUserService;

        public CreateRunCommandHandler(IMapper mapper, IRouteService routeService,
            ICurrentUserService currentUserService, IPointService pointService,
            IRepository<Run> repository, IRunService runService)
        {
            _mapper = mapper;
            _routeService = routeService;
            _currentUserService = currentUserService;
            _pointService = pointService;
            _repository = repository;
            _runService = runService;
        }

        public async Task<RunDto> Handle(CreateRunCommand request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUserService.UserId);

            var run = await _routeService.CreateRun(request.RouteId, userId);

            var result = _mapper.Map<RunDto>(run);
            return result;
        }
    }
}

using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Application.Common.Routes.Queries.GetRoutes;

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
        private readonly IRouteService _routeService;
        private readonly IRunService _runService;
        private readonly ICurrentUserService _currentUserService;

        public CreateRunCommandHandler(IMapper mapper, IRouteService routeService, IRunService runService, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _routeService = routeService;
            _runService = runService;
            _currentUserService = currentUserService;
        }

        public async Task<RunDto> Handle(CreateRunCommand request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUserService.UserId);

            var activeRun = await _runService.GetActiveRun(userId);

            if (activeRun is not null)
            {
                await _runService.CancelRun(activeRun);
            }

            var run = await _routeService.CreateRun(request.RouteId, userId);

            var result = _mapper.Map<RunDto>(run);

            return result;
        }
    }
}

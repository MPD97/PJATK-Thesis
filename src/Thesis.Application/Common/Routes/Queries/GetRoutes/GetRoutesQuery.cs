using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Thesis.Domain.Enums;

namespace Thesis.Application.Common.Routes.Queries.GetRoutes
{
    public class GetRoutesQuery : IRequest<GetRoutesVM>
    {
        public decimal TopLeftLat { get; init; }
        public decimal TopLeftLon { get; init; }

        public decimal BottomRightLat { get; init; }
        public decimal BottomRightLon { get; init; }

        public int Amount { get; init; } = 50;
    }

    public class GetRoutesQueryHandler : IRequestHandler<GetRoutesQuery, GetRoutesVM>
    {
        private readonly IMapper _mapper;
        private readonly IRouteService _routeService;

        public GetRoutesQueryHandler(IMapper mapper, IRouteService routeService)
        {
            _mapper = mapper;
            _routeService = routeService;
        }

        public async Task<GetRoutesVM> Handle(GetRoutesQuery request, CancellationToken cancellationToken)
        {
            var routes = _routeService.GetRoutesInBoundaries(request.TopLeftLat, request.TopLeftLon, request.BottomRightLat, request.BottomRightLon, request.Amount);

            var dtos = await _mapper.ProjectTo<RouteDto>(routes)
                .ToListAsync(cancellationToken);

            return new GetRoutesVM(dtos);
        }
    }
}

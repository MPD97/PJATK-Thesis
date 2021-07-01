using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Application.Common.Routes.Queries.GetRoutes;

namespace Thesis.Application.Common.Routes.Queries.GetRoute
{
    public class GetRouteQuery : IRequest<GetRouteVM>
    {
        public int RouteId { get; init; }
    }

    public class GetRouteQueryHandler : IRequestHandler<GetRouteQuery, GetRouteVM>
    {
        private readonly IMapper _mapper;
        private readonly IRouteService _routeService;

        public GetRouteQueryHandler(IMapper mapper, IRouteService routeService)
        {
            _mapper = mapper;
            _routeService = routeService;
        }

        public async Task<GetRouteVM> Handle(GetRouteQuery request, CancellationToken cancellationToken)
        {
            //TODO: PipelineValidation
            var route = _routeService.GetRouteById(request.RouteId);

            var dto = await _mapper.ProjectTo<RouteDto>(route)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return new GetRouteVM(dto);
        }
    }
}

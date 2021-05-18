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

namespace Thesis.Application.Common.Routes.Queries
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
        private readonly IRepository<Route> _repository;

        public GetRoutesQueryHandler(IMapper mapper, IRepository<Route> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetRoutesVM> Handle(GetRoutesQuery request, CancellationToken cancellationToken)
        {
            var routes = _repository
                 .GetAll()
                 .AsNoTracking()
                 .Where(r => request.TopLeftLat >= r.TopLeftLatitude)
                 .Where(r => request.TopLeftLon <= r.TopLeftLongitude)
                 .Where(r => request.BottomRightLat <= r.BottomLeftLatitude)
                 .Where(r => request.BottomRightLon >= r.BottomLeftLongitude)
                 .Take(request.Amount);

            var c = routes.ToArray();

            var dtos = await _mapper.ProjectTo<RouteDto>(routes)
                .ToListAsync(cancellationToken);

            return new GetRoutesVM(dtos);
        }
    }
}

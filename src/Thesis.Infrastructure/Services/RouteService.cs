using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;

namespace Thesis.Infrastructure.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRepository<Route> _repository;

        public RouteService(IRepository<Route> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<Route>> GetRoutesInBoundaries(decimal topLeftLat, decimal topLefrLon, decimal topRightLat, decimal topRightLon, decimal bottomLeftLat, decimal bottomLeftLon, decimal bottomRightLat, decimal bottomRightLon, int take = 50)
        {
            throw new NotImplementedException();
        }
    }
}

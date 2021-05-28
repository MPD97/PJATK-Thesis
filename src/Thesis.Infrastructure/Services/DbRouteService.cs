using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;
using Thesis.Domain.Enums;

namespace Thesis.Infrastructure.Services
{
    public class DbRouteService : IRouteService
    {
        private readonly IRepository<Route> _repository;

        public DbRouteService(IRepository<Route> repository)
        {
            _repository = repository;
        }

        public  IQueryable<Route> GetRoutesInBoundaries(decimal topLeftLat, decimal topLeftLon, decimal bottomRightLat, decimal bottomRightLon, int take = 50)
        {
            var routes = _repository
                .GetAll()
                .AsNoTracking()
                .Include(x => x.Points)
                .Where(r => r.Status == RouteStatus.Accepted)
                .Where(r => topLeftLat >= r.TopLeftLatitude)
                .Where(r => topLeftLon <= r.TopLeftLongitude)
                .Where(r => bottomRightLat <= r.BottomLeftLatitude)
                .Where(r => bottomRightLon >= r.BottomLeftLongitude)
                .Take(take);

            return routes;
        }
    }
}

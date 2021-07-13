using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;
using Thesis.Domain.Enums;
using Thesis.Domain.Exceptions;

namespace Thesis.Infrastructure.Services
{
    public class DbRouteService : IRouteService
    {
        private readonly IRepository<Route> _routeRepository;
        private readonly IDateTime _dateTime;

        public DbRouteService(IRepository<Route> routeRepository, IDateTime dateTime)
        {
            _routeRepository = routeRepository;
            _dateTime = dateTime;
        }

        public async Task<Run> CreateRun(int routeId, int userId)
        {
            var route = await _routeRepository
                .FindBy(r => r.Id == routeId)
                .Where(r => r.Status == RouteStatus.Accepted)
                .Include(r => r.Points)
                .FirstOrDefaultAsync();

            if (route is null)
            {
                //TODO: PipelineValidation
                throw new Exception($"Route with id: {routeId} not exists or is not accepted.");
            }

            using (var transaction = _routeRepository.BeginTransaction())
            {
                var run = route.CreateRun(userId, _dateTime.Now);

                var firstPoint = route.Points.Where(p => p.Order == 1).FirstOrDefault();
                if (firstPoint is null)
                {
                    //rollback
                    throw new DomainLayerException($"Cannot find first point for route: {route.Id}");
                }

                var completedPoint = run.CompletePoint(firstPoint, _dateTime.Now);

                await _routeRepository.SaveChangesAsync();
                transaction.Commit();

                return run;
            }
        }

        public IQueryable<Route> GetRouteById(int routeId)
        {
            var route = _routeRepository
                .FindBy(r => r.Id == routeId)
                .AsNoTracking()
                .Include(r => r.Points);

            return route;
        }

        public IQueryable<Route> GetRoutesInBoundaries(decimal topLeftLat, decimal topLeftLon, decimal bottomRightLat, decimal bottomRightLon, int take = 50)
        {
            var routes = _routeRepository
                .GetAll()
                .AsNoTracking()
                .Include(r => r.Points)
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

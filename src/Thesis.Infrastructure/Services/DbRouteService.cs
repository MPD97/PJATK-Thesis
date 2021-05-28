using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;
using Thesis.Domain.Enums;
using Thesis.Infrastructure.Identity;

namespace Thesis.Infrastructure.Services
{
    public class DbRouteService : IRouteService
    {
        private readonly IRepository<Route> _repository;
        private readonly IDateTime _dateTime;

        public DbRouteService(IRepository<Route> repository, IDateTime dateTime)
        {
            _repository = repository;
            _dateTime = dateTime;
        }

        public async Task<Run> CreateRun(int routeId, int userId)
        {
            var route = await _repository
                .FindBy(r => r.Id == routeId)
                .Where(r => r.Status == RouteStatus.Accepted)
                .FirstOrDefaultAsync();

            if (route == null)
            {
                throw new Exception($"Route with id: {routeId} not exists or is not accepted.");
            }

            var run = route.CreateRun(userId, _dateTime.Now);

            await _repository.SaveChangesAsync();

            return run;
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

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;

namespace Thesis.Infrastructure.Services
{
    public class DbPointService : IPointService
    {
        private readonly IRepository<Point> _repository;

        public DbPointService(IRepository<Point> repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Point>> GetRoutePointsNoTracking(int routeId)
        {
            var points = await _repository
                .GetAll()
                .AsNoTracking()
                .Where(p => p.Id == routeId)
                .OrderBy(x => x.Order)
                .ToArrayAsync();

            return points;
        }

        public async Task<Point> GetPointNoTracking(int routeId, int pointOrder)
        {
            var point = await _repository
                  .FindBy(p => p.RouteId == routeId)
                  .AsNoTracking()
                  .Where(p => p.Order == pointOrder)
                  .FirstOrDefaultAsync();

            return point;
        }

        public async Task<Point> GetPointNoTracking(int pointId)
        {
            var point = await _repository
              .FindBy(p => p.Id == pointId)
              .AsNoTracking()
              .FirstOrDefaultAsync();

            return point;
        }

        public async Task<Point> GetPoint(int pointId)
        {
            var point = await _repository
              .FindBy(p => p.Id == pointId)
              .Include(p => p.Route)
              .Include(p => p.NextPoint)
              .FirstOrDefaultAsync();

            return point;
        }
    }
}

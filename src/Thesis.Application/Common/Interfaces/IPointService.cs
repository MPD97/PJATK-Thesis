using System.Collections.Generic;
using System.Threading.Tasks;
using Thesis.Domain.Entities;

namespace Thesis.Application.Common.Interfaces
{
    public interface IPointService
    {
        Task<Point> GetPointNoTracking(int pointId);
        Task<Point> GetPointNoTracking(int routeId, int pointOrder);
        Task<ICollection<Point>> GetRoutePointsNoTracking(int routeId);
        Task<Point> GetPoint(int pointId);
    }
}

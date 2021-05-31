using System.Collections.Generic;
using System.Threading.Tasks;
using Thesis.Domain.Entities;

namespace Thesis.Application.Common.Interfaces
{
    public interface IPointService
    {
        Task<Point> GetPoint(int routeId, int pointOrder);

        Task<ICollection<Point>> GetRoutePoints(int routeId);

    }
}

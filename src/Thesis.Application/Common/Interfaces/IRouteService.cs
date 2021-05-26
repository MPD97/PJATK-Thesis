using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Domain.Entities;

namespace Thesis.Application.Common.Interfaces
{
    public interface IRouteService
    {
        Task<IReadOnlyList<Route>> GetRoutesInBoundaries(decimal topLeftLat, decimal topLefrLon, decimal bottomRightLat, decimal bottomRightLon, int take = 50);

    }
}

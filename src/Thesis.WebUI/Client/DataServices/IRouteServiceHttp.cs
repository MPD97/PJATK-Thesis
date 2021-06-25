using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thesis.Application.Common.Routes.Queries.GetRoutes;

namespace Thesis.WebUI.Client.DataServices
{
    public interface IRouteServiceHttp
    {
        public Task<GetRoutesVM> GetRoutes(decimal topLeftLat, decimal topLeftLon, decimal bottomRightLat, decimal bottomRightLon, decimal amount);

        public Task<string> GetRoutesGeoJson(decimal topLeftLat, decimal topLeftLon, decimal bottomRightLat, decimal bottomRightLon, decimal amount);

        public Task<string> GetRouteGeoJson(int routeId);

    }
}

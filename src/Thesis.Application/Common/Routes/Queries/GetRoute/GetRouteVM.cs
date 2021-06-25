using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Routes.Queries.GetRoutes;

namespace Thesis.Application.Common.Routes.Queries.GetRoute
{
    public class GetRouteVM
    {
        public RouteDto Route { get; protected set; }

        public GetRouteVM(RouteDto route)
        {
            Route = route;
        }
    }

}

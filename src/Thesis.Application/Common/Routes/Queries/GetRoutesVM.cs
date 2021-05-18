using System.Collections.Generic;

namespace Thesis.Application.Common.Routes.Queries
{
    public class GetRoutesVM
    {
        public IReadOnlyList<RouteDto> Routes { get; protected set; }

        public GetRoutesVM(IReadOnlyList<RouteDto> routes)
        {
            Routes = routes;
        }
    }
}

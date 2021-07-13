using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Application.Common.Extensions;
using Thesis.Application.Common.Models.GeoJson.Route;
using Thesis.Application.Common.Routes.Queries.GetRoute;
using Thesis.Application.Common.Routes.Queries.GetRoutes;
using Thesis.WebUI.Server.Attributes;

namespace Thesis.WebUI.Server.Controllers
{
    [AllowAnonymous]
    public class RouteController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetRoutesVM>> Get([FromQuery] GetRoutesQuery query, CancellationToken token)
        {
            var result = await Mediator.Send(query, token);

            return Ok(result);
        }

        [HttpGet, HttpHeader("Format", "GeoJson")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GJRouteVM>>> GetGeoJson([FromQuery] GetRoutesQuery query, CancellationToken token)
        {
            var result = await Mediator.Send(query, token);

            var models = result.Routes
                .ToGeoJsonVM()
                .ToArray();

            return Ok(models);
        }

        [HttpGet("{routeId:int}"), HttpHeader("Format", "GeoJson")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GJRouteVM>>> GetGeoJson([FromRoute] GetRouteQuery query, CancellationToken token)
        {
            var result = await Mediator.Send(query, token);

            if (result.Route is null)
                return NotFound("Route not found");

            var model = result.Route
                .ToGeoJsonVM();

            return Ok(model);
        }
    }
}

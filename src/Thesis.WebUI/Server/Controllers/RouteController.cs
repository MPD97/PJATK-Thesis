using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Application.Common.Extensions;
using Thesis.Application.Common.Models.GeoJson.Line;
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
        public async Task<ActionResult<IReadOnlyList<GJLine>>> GetGeoJson([FromQuery] GetRoutesQuery query, CancellationToken token)
        {
            var result = await Mediator.Send(query, token);

            var gjs = result.Routes.Select(x => x.ToGeoJson()).ToList();

            return Ok(gjs);
        }

    }
}

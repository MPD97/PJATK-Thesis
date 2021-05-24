﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Application.Common.Extensions;
using Thesis.Application.Common.Models.GeoJson.Base;
using Thesis.Application.Common.Models.GeoJson.Line;
using Thesis.Application.Common.Models.GeoJson.Route;
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
        public async Task<ActionResult<GJSourceResult>> GetGeoJson([FromQuery] GetRoutesQuery query, CancellationToken token)
        {
            var result = await Mediator.Send(query, token);

            var gjs = result.Routes
                .ToGeoJson()
                .ToArray();

            return Ok(new GJSourceResult(gjs));
        }
        [HttpGet, HttpHeader("Format", "GeoJsonQuick")]
        public async Task<ActionResult<GJSourceResult>> GetGeoJsonQuick([FromQuery] GetRoutesQuery query, CancellationToken token)
        {
            var result = await Mediator.Send(query, token);

            var gjs = result.Routes
                .ToGeoJsonResult()
                .ToArray();

            return Ok(new GJSourceLineResultVM(gjs));
        }
        [HttpGet, HttpHeader("Format", "GeoJsonVM")]
        public async Task<ActionResult<ICollection<RouteVM>>> GetGeoJsonVM([FromQuery] GetRoutesQuery query, CancellationToken token)
        {
            var result = await Mediator.Send(query, token);

            var gjs = result.Routes
                .ToGeoJsonVM()
                .ToArray();

            return Ok(gjs);
        }
    }
}

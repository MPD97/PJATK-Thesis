using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Application.Common.Routes.Queries.GetRoutes;

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
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Thesis.Application.Common.Routes.Queries;

namespace Thesis.WebUI.Server.Controllers
{
    [AllowAnonymous]
    public class RouteController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetRoutesVM>> Get([FromQuery] GetRoutesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }
    }
}

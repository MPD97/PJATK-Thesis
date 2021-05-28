using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Application.Common.Routes.Commands.CreateRun;

namespace Thesis.WebUI.Server.Controllers
{
    [Authorize]
    public class RunController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<RunDto>> CreateRun(CreateRunCommand command, CancellationToken token)
        {
            var result = await Mediator.Send(command, token);

            return Ok(result);
        }
    }
}

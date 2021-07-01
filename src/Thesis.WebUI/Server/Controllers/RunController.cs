using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Application.Common.Models;
using Thesis.Application.Common.Routes.Commands.CreateRun;
using Thesis.Application.Common.Routes.Commands.ReachPoint;

namespace Thesis.WebUI.Server.Controllers
{
    [Authorize]
    public class RunController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ApiResult<RunDto>>> CreateRun(CreateRunCommand command, CancellationToken token)
        {
            var result = await Mediator.Send(command, token);

            return Ok(ApiResult<RunDto>.Success(result));
        }

        [HttpPost("{runId:int}")]
        public async Task<ActionResult<ApiResult<RunDto>>> ReachCheckpoint(int runId, ReachPointCommand command, CancellationToken token)
        {
            command.RunId = runId;
            var result = await Mediator.Send(command, token);

            return Ok(ApiResult<RunDto>.Success(result));
        }
    }
}

using System.Net.Http;
using System.Threading.Tasks;
using Thesis.Application.Common.Models;
using Thesis.Application.Common.Routes.Commands.CreateRun;

namespace Thesis.WebUI.Client.DataServices
{
    public interface IRunServiceHttp
    {
        public Task<ApiResult<RunDto>> CreateRun(int routeId, decimal latitude, decimal longitude, int accuracy);
    }
}

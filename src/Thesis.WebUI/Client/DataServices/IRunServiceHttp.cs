using System.Net.Http;
using System.Threading.Tasks;
using Thesis.Application.Common.Routes.Commands.CreateRun;

namespace Thesis.WebUI.Client.DataServices
{
    public interface IRunServiceHttp
    {
        public Task<RunDto> CreateRun(int routeId, decimal latitude, decimal longitude, int accuracy);
    }
}

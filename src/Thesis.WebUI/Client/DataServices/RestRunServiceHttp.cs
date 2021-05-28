using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Routes.Commands.CreateRun;

namespace Thesis.WebUI.Client.DataServices
{
    public class RestRunServiceHttp : IRunServiceHttp
    {
        private readonly HttpClient _http;

        public RestRunServiceHttp(HttpClient http)
        {
            _http = http;
        }

        public async Task<RunDto> CreateRun(int routeId, decimal latitude, decimal longitude, int accuracy)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Run");

            var command = new CreateRunCommand(routeId, latitude, longitude, accuracy);

            var text = JsonConvert.SerializeObject(command);

            request.Content = new StringContent(text, Encoding.UTF8/*, "application/json"*/);

            var httpResponse = await _http.SendAsync(request);

            var result = await httpResponse.Content.ReadFromJsonAsync<RunDto>();

            return result;
        }
    }
}

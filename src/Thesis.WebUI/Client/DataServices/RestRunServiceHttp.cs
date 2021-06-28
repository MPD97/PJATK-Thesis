using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Thesis.Application.Common.Routes.Commands.CreateRun;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Thesis.Application.Common.Models;
using Thesis.Application.Common.Routes.Commands.ReachPoint;

namespace Thesis.WebUI.Client.DataServices
{
    public class RestRunServiceHttp : IRunServiceHttp
    {
        private readonly HttpClient _http;
        private readonly IAccessTokenProvider _tokenProvider;

        public RestRunServiceHttp(HttpClient http, IAccessTokenProvider tokenProvider)
        {
            _http = http;
            _tokenProvider = tokenProvider;
        }

        public async Task<ApiResult<RunDto>> CreateRun(int routeId, decimal latitude, decimal longitude, int accuracy)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Run");

            var command = new CreateRunCommand(routeId, latitude, longitude, accuracy);

            var text = JsonConvert.SerializeObject(command);

            request.Content = new StringContent(text, Encoding.UTF8, "application/json");

            var tokenResult = await _tokenProvider.RequestAccessToken();
            if (tokenResult.TryGetToken(out var token) == false)
            {
                return null;
            }
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Value);


            var httpResponse = await _http.SendAsync(request);

            var result = await httpResponse.Content.ReadFromJsonAsync<ApiResult<RunDto>>();

            return result;
        }

        public async Task<ApiResult<RunDto>> ReachPoint(int runId, int pointId, decimal latitude, decimal longitude, int accuracy)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Run/{runId}");

            var command = new ReachPointCommand(pointId, latitude, longitude, accuracy);

            var text = JsonConvert.SerializeObject(command);

            request.Content = new StringContent(text, Encoding.UTF8, "application/json");

            var tokenResult = await _tokenProvider.RequestAccessToken();
            if (tokenResult.TryGetToken(out var token) == false)
            {
                return null;
            }
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Value);


            var httpResponse = await _http.SendAsync(request);

            var result = await httpResponse.Content.ReadFromJsonAsync<ApiResult<RunDto>>();

            return result;
        }
    }
}

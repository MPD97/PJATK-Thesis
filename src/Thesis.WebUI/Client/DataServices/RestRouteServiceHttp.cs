using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Thesis.Application.Common.Routes.Queries.GetRoute;
using Thesis.Application.Common.Routes.Queries.GetRoutes;

namespace Thesis.WebUI.Client.DataServices
{
    public class RestRouteServiceHttp : IRouteServiceHttp
    {
        private readonly HttpClient _http;

        public RestRouteServiceHttp(HttpClient http)
        {
            _http = http;
        }

        public async Task<GetRoutesVM> GetRoutes(decimal topLeftLat, decimal topLeftLon, decimal bottomRightLat, decimal bottomRightLon, decimal amount)
        {
            var result = await _http.GetFromJsonAsync<GetRoutesVM>($"api/Route?TopLeftLat={topLeftLat.ToString(CultureInfo.InvariantCulture)}&TopLeftLon={topLeftLon.ToString(CultureInfo.InvariantCulture)}&BottomRightLat={bottomRightLat.ToString(CultureInfo.InvariantCulture)}&BottomRightLon={bottomRightLon.ToString(CultureInfo.InvariantCulture)}");

            return result;
        }

        public async Task<string> GetRoutesGeoJson(decimal topLeftLat, decimal topLeftLon, decimal bottomRightLat, decimal bottomRightLon, decimal amount)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Route?TopLeftLat={topLeftLat.ToString(CultureInfo.InvariantCulture)}&TopLeftLon={topLeftLon.ToString(CultureInfo.InvariantCulture)}&BottomRightLat={bottomRightLat.ToString(CultureInfo.InvariantCulture)}&BottomRightLon={bottomRightLon.ToString(CultureInfo.InvariantCulture)}");
            request.Headers.Add("Format", "GeoJson");

            var httpResponse = await _http.SendAsync(request);

            var result = await httpResponse.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<string> GetRouteGeoJson(int routeId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Route/{routeId}");
            request.Headers.Add("Format", "GeoJson");

            var httpResponse = await _http.SendAsync(request);

            var result = await httpResponse.Content.ReadAsStringAsync();

            return result;
        }
    }
}

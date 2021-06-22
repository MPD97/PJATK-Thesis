using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Thesis.Application.Common.Routes.Commands.CreateRun;
using Thesis.Application.Common.Routes.Queries.GetRoutes;
using Thesis.WebUI.Client.DataServices;

namespace Thesis.WebUI.Client.Pages
{
    public partial class Map
    {
        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private IRouteServiceHttp _routeService { get; set; }

        [Inject]
        private IRunServiceHttp _runService { get; set; }

        [Inject]
        private NavigationManager UriHelper { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }
        private ClaimsPrincipal User { get; set; }
        private bool IsAuthenticated { get; set; }

        protected override async Task OnInitializedAsync()
        {
            User = (await AuthenticationState).User;

            if (User.Identity.IsAuthenticated)
            {
                IsAuthenticated = true;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _jsRuntime.InvokeVoidAsync("createMap");

                await _jsRuntime.InvokeVoidAsync("mapHelper.init", DotNetObjectReference.Create(this));
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        [JSInvokable("GetRoutesGeoJson")]
        public async Task<string> GetRoutesGeoJson(decimal topLeftLat, decimal topLeftLon, decimal bottomRightLat, decimal bottomRightLon, decimal currentZoom)
        {
            return await _routeService.GetRoutesGeoJson(topLeftLat, topLeftLon, bottomRightLat, bottomRightLon, 50);
        }

        [JSInvokable("CreateRun")]
        public async Task<RunDto> CreateRun(int routeId, decimal latitude, decimal longitude, int accuracy)
        {
            if (!IsAuthenticated)
            {
                UriHelper.NavigateTo("login");
            }
            var result = await _runService.CreateRun(routeId, latitude, longitude, accuracy);

            return result;
        }
    }
}

﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Thesis.Application.Common.Routes.Queries.GetRoutes;
using Thesis.WebUI.Client.DataServices;

namespace Thesis.WebUI.Client.Pages
{
    public partial class Map
    {
        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private IRouteServiceHttp _service { get; set; }
        
        [Inject]
        private IConfiguration _configuration { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _jsRuntime.InvokeVoidAsync("mapInitializer.create", DotNetObjectReference.Create(this));

                await _jsRuntime.InvokeVoidAsync("mapHelper.init", DotNetObjectReference.Create(this));
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        [JSInvokable("GetOSMServerAddress")]
        public string GetOSMServerAddress()
        {
            return _configuration["osm_server_base_uri"];
        }

        [JSInvokable("GetRoutes")]
        public async Task<GetRoutesVM> GetRoutes(decimal topLeftLat, decimal topLeftLon, decimal bottomRightLat, decimal bottomRightLon, decimal currentZoom)
        {
            return await _service.GetRoutes(topLeftLat, topLeftLon, bottomRightLat, bottomRightLon, 50);
        }

        [JSInvokable("GetRoutesGeoJson")]
        public async Task<string> GetRoutesGeoJson(decimal topLeftLat, decimal topLeftLon, decimal bottomRightLat, decimal bottomRightLon, decimal currentZoom)
        {
            return await _service.GetRoutesGeoJson(topLeftLat, topLeftLon, bottomRightLat, bottomRightLon, 50);
        }
    }
}
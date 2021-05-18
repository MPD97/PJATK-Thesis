﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;

namespace Thesis.Infrastructure.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRepository<Route> _repository;

        public RouteService(IRepository<Route> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<Route>> GetRoutesInBoundaries(decimal topLeftLat, decimal topLeftLon, decimal bottomRightLat, decimal bottomRightLon, int take = 50)
        {
            var routes = await _repository
                .GetAll()
                .AsNoTracking()
                .Where(r => topLeftLat <= r.TopLeftLatitude)
                .Where(r => topLeftLon >= r.TopLeftLongitude)
                .Where(r => topLeftLat >= r.BottomLeftLatitude)
                .Where(r => topLeftLat <= r.BottomLeftLongitude)
                .Take(50)
                .ToArrayAsync();

            return routes;
        }
    }
}
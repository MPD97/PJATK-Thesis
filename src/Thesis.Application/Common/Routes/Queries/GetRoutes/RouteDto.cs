﻿using AutoMapper;
using System.Collections.Generic;
using Thesis.Application.Common.Mappings;
using Thesis.Domain.Entities;
using Thesis.Domain.Enums;

namespace Thesis.Application.Common.Routes.Queries.GetRoutes
{
    public class RouteDto : IMapFrom<Route>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RouteDifficulty Difficulty { get; set; }
        public int LengthInMeters { get; set; }
        public IList<PointDto> Points { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Route, RouteDto>();
        }
    }
}

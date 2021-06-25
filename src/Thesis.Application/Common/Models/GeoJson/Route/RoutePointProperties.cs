﻿using Thesis.Application.Common.Models.GeoJson.Base;
using Thesis.Domain.Enums;

namespace Thesis.Application.Common.Models.GeoJson.Route
{

    public class RouteLineProperties : GJPropertiesBase
    {
        public RoutePointType Type { get; set; }
        public int RouteId { get; set; }
    }
    public class RoutePointProperties : GJPropertiesBase
    {
        public RoutePointType Type { get; set; }
        public int Order { get; set; }
    }
    public enum RoutePointType 
    {
        Start = 0,  //  Starting Point
        Other = 2,  //  Other than Start, Next, End and Line
        End = 3,    //  Finishing point

        Line = 10   //  Dotted line between points
    }
}
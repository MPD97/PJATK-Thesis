using Thesis.Application.Common.Models.GeoJson.Base;
using Thesis.Domain.Enums;

namespace Thesis.Application.Common.Models.GeoJson.Route
{
    public class RouteProperties : GJPropertiesBase
    {
        public RoutePointType Type { get; set; }
    }
    public enum RoutePointType 
    {
        Start = 0,  //  Starting Point
        Other = 2,  //  Other than Start, Next, End and Line
        End = 3,    //  Finishing point

        Line = 10   //  Dotted line between points
    }
}

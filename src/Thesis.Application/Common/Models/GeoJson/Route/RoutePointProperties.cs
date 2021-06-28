using Thesis.Application.Common.Models.GeoJson.Base;
using Thesis.Domain.Enums;

namespace Thesis.Application.Common.Models.GeoJson.Route
{

    public class RouteLineProperties : GJPropertiesBase
    {
        public RoutePointType Type { get; set; }

        public RouteLineProperties()
        {

        }
        public RouteLineProperties(RoutePointType type)
        {
            Type = type;
        }
    }
    public class RoutePointProperties : GJPropertiesBase
    {
        public RoutePointType Type { get; set; }
        public int Order { get; set; }
        public int PointId { get; set; }

        public RoutePointProperties()
        {

        }
        public RoutePointProperties(RoutePointType type, int order, int pointId)
        {
            Type = type;
            Order = order;
            PointId = pointId;
        }
    }
    public class RoutePointPropertiesWithRouteId : RoutePointProperties
    {
        public int RouteId { get; set; }

        public RoutePointPropertiesWithRouteId()
        {

        }
        public RoutePointPropertiesWithRouteId(RoutePointType type, int order, int pointId, int routeId) : base(type, order, pointId)
        {
            RouteId = routeId;
        }
    }
    public enum RoutePointType
    {
        Start = 0,  //  Starting Point
        Other = 2,  //  Other than Start, Next, End and Line
        End = 3,    //  Finishing point

        Line = 10   //  Dotted line between points
    }
}

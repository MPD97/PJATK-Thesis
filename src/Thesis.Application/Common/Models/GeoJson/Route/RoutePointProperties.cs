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
    public class RoutePointPropertiesFirstPoint : RoutePointProperties
    {
        public int RouteId { get; set; }

        public string RouteName { get; set; }
        public string RouteDescription { get; set; }
        public int RouteLength { get; set; }
        public RouteDifficulty RouteDifficulty { get; set; }

        public RoutePointPropertiesFirstPoint()
        {

        }


        public RoutePointPropertiesFirstPoint(RoutePointType type, int order, int pointId, int routeId, string routeName, string routeDescription, int routeLength, RouteDifficulty routeDifficulty) : base(type, order, pointId)
        {
            RouteId = routeId;
            RouteName = routeName;
            RouteDescription = routeDescription;
            RouteLength = routeLength;
            RouteDifficulty = routeDifficulty;
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

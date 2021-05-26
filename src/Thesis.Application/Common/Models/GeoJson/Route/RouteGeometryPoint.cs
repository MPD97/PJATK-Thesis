using Thesis.Application.Common.Models.GeoJson.Base;

namespace Thesis.Application.Common.Models.GeoJson.Route
{
    public class RouteGeometryPoint : RouteGeometry
    {
        public new string Type { get; set; } = "LineString";
        public new decimal[] Coordinates { get; set; }
    }
}

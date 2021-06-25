using Thesis.Application.Common.Models.GeoJson.Base;

namespace Thesis.Application.Common.Models.GeoJson.Route
{
    public class RouteData : GJDataBase
    {
        public new RouteGeometry Geometry { get; set; } = new();
        public new GJPropertiesBase Properties { get; set; }
    }
}

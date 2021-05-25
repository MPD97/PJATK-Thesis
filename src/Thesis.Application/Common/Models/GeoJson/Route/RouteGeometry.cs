using System.Collections.Generic;
using Thesis.Application.Common.Models.GeoJson.Base;

namespace Thesis.Application.Common.Models.GeoJson.Route
{
    public class RouteGeometry : GJGeometryBase
    {
        public new string Type { get; set; } = "LineString";
        public new ICollection<decimal[]> Coordinates { get; set; }

    }
}

using Thesis.Application.Common.Models.GeoJson.Base;

namespace Thesis.Application.Common.Models.GeoJson.Line
{
    public class GJGeometryLine : GJGeometryBase
    {
        public new string Type { get; set; } = "LineString";
    }
}

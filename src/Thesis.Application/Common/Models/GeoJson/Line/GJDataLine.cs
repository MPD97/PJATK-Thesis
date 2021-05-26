using Thesis.Application.Common.Models.GeoJson.Base;

namespace Thesis.Application.Common.Models.GeoJson.Line
{
    public class GJDataLine : GJDataBase
    {
        public new GJGeometryLine Geometry { get; set; } = new();
        public new GJPropertiesLine Properties { get; set; } = new();
    }
}

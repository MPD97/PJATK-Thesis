namespace Thesis.Application.Common.Models.GeoJson.Base
{
    public abstract class GJGeometryBase
    {
        public string Type { get; set; }
        public decimal[,] Coordinates { get; set; }
    }
}

namespace Thesis.Application.Common.Models.GeoJson.Base
{
    public abstract class GJSourceBase
    {
        public string Type { get; set; } = "geojson";
        public virtual GJDataBase Data { get; set; } 
    }
}

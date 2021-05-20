namespace Thesis.Application.Common.Models.GeoJson.Base
{
    public abstract class GJBase
    {
        public string Type { get; set; } = "geojson";
        public virtual GJDataBase Data { get; set; } 
    }
}

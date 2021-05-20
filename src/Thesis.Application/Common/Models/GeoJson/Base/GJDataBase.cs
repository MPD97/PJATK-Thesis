namespace Thesis.Application.Common.Models.GeoJson.Base
{
    public abstract class GJDataBase
    {
        public string Type { get; set; } = "Feature";
        public virtual GJPropertiesBase Properties { get; set; } = new();
        public virtual GJGeometryBase Geometry { get; set; } = new();
    }
}

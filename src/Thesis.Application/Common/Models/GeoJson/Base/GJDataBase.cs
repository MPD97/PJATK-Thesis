namespace Thesis.Application.Common.Models.GeoJson.Base
{
    public abstract class GJDataBase
    {
        public virtual string Type { get; set; } = "Feature";
        public virtual GJPropertiesBase Properties { get; set; } 
        public virtual GJGeometryBase Geometry { get; set; } 
    }
}

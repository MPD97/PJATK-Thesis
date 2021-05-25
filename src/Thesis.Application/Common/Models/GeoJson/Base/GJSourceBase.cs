using System.Collections;

namespace Thesis.Application.Common.Models.GeoJson.Base
{
    public abstract class GJSourceBase
    {
        public virtual string Type { get; set; } = "geojson";
        public virtual GJDataBase Data { get; set; }
    }

}

using System.Collections.Generic;

namespace Thesis.Application.Common.Models.GeoJson.Base
{
    public abstract class GJSourceBase
    {
        public string Type { get; set; } = "geojson";
        public virtual GJDataBase Data { get; set; } 
    }
    public class  GJSourceResult
    {
        public virtual IReadOnlyCollection<GJSourceBase> Sources { get; set; }

        public GJSourceResult()
        {

        }

        public GJSourceResult(IReadOnlyCollection<GJSourceBase> sources)
        {
            Sources = sources;
        }
    }
}

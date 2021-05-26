using System.Collections.Generic;

namespace Thesis.Application.Common.Models.GeoJson.Base
{
    public abstract class GJFeaturesBase
    {
        public virtual string Type { get; set; } = "FeatureCollection";
        public virtual ICollection<GJDataBase> Features { get; set; }
    }

}

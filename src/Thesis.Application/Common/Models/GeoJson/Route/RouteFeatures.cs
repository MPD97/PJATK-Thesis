using System.Collections.Generic;
using Thesis.Application.Common.Models.GeoJson.Base;

namespace Thesis.Application.Common.Models.GeoJson.Route
{
    public class RouteFeatures : GJFeaturesBase
    {
        public override string Type { get; set; } = "FeatureCollection";
        public new IEnumerable<RouteData> Features { get; set; }
    }
}

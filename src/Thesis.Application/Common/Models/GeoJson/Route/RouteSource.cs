using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Models.GeoJson.Base;

namespace Thesis.Application.Common.Models.GeoJson.Route
{
    public class RouteSource : GJSourceBase
    {
        public new RouteFeatures Data { get; set; } = new();
        public new string Type { get; set; } = "geojson";
    }
}

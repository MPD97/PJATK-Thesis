using System.Collections.Generic;

namespace Thesis.Application.Common.Models.GeoJson.Base
{
    public abstract class GJGeometryBase
    {
        public string Type { get; set; }
        public ICollection<decimal[]> Coordinates { get; set; }
    }
}

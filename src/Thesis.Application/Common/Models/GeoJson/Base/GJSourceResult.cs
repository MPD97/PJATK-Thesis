using System.Collections.Generic;

namespace Thesis.Application.Common.Models.GeoJson.Base
{
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

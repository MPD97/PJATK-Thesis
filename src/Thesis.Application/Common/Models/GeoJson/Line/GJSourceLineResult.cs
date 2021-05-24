using System.Collections.Generic;

namespace Thesis.Application.Common.Models.GeoJson.Line
{
    public class GJSourceLineResult
    {
        public virtual IReadOnlyCollection<GJSourceLine> Sources { get; set; }

        public GJSourceLineResult()
        {

        }

        public GJSourceLineResult(IReadOnlyCollection<GJSourceLine> sources)
        {
            Sources = sources;
        }
    }
}

using System.Collections.Generic;

namespace Thesis.Application.Common.Models.GeoJson.Line
{
    public class GJSourceLineResultVM
    {
        public virtual IReadOnlyCollection<GJSourceLineResult> Results { get; set; }

        public GJSourceLineResultVM()
        {

        }

        public GJSourceLineResultVM(IReadOnlyCollection<GJSourceLineResult> results)
        {
            Results = results;
        }
    }
}

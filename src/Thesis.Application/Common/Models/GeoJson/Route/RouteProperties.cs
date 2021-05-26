using Thesis.Application.Common.Models.GeoJson.Base;
using Thesis.Domain.Enums;

namespace Thesis.Application.Common.Models.GeoJson.Route
{
    public class RouteProperties : GJPropertiesBase
    {
        public string Title { get; set; }
        public RouteDifficulty Difficulty { get; set; }
    }
}

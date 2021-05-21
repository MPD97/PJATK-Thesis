using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Models.GeoJson.Line;
using Thesis.Application.Common.Routes.Queries.GetRoutes;

namespace Thesis.Application.Common.Extensions
{
    public static class GeoJsonExtension
    {
        public static GJSourceLine ToGeoJson(this RouteDto route)
        {
            var geoJson = new GJSourceLine();

            var sortedPoints = route.Points
                .OrderBy(o => o.Order)
                .Select(o => new decimal[2] { o.Longitude, o.Latitude })
                .ToArray();

            geoJson.Data.Geometry.Coordinates = sortedPoints;

            return geoJson;
        }

        public static IEnumerable<GJSourceLine> ToGeoJson(this IEnumerable<RouteDto> routes)
        {
            var geoJsons = new List<GJSourceLine>(routes.Count());

            foreach (var route in routes)
            {
                geoJsons.Add(route.ToGeoJson());
            }

            return geoJsons;
        }
    }
}

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Models.GeoJson.Line;
using Thesis.Application.Common.Routes.Queries.GetRoutes;

namespace Thesis.Application.Common.Extensions
{
    public static class GeoJsonExtension
    {
        public static GJLine ToGeoJson(this RouteDto rote)
        {
            var geoJson = new GJLine();

            var sortedPoints = rote.Points
                .OrderBy(o => o.Order)
                .Select(o => new decimal[2] { o.Longitude, o.Latitude })
                .To2DArray();

            geoJson.Data.Geometry.GeoJsonCoordinates = sortedPoints;

            return geoJson;
        }
    }
}

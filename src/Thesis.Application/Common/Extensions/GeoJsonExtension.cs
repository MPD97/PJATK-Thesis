using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Models.GeoJson.Line;
using Thesis.Application.Common.Models.GeoJson.Route;
using Thesis.Application.Common.Routes.Queries.GetRoutes;

namespace Thesis.Application.Common.Extensions
{
    public static class GeoJsonExtension
    {
        
        public static GJSourceLine ToGeoJson(this RouteDto route)
        {
            var result = new GJSourceLine();

            var sortedPoints = route.Points
                .OrderBy(o => o.Order)
                .Select(o => new decimal[2] { o.Longitude, o.Latitude })
                .ToArray();

            result.Data.Geometry.Coordinates = sortedPoints;

            return result;
        }

        public static GJSourceLineResult ToGeoJsonResult(this RouteDto route)
        {
            var result = new GJSourceLineResult();

            result.SourceId = route.Id;

            result.Source = route.ToGeoJson();

            return result;
        }

        public static RouteVM ToGeoJsonVM(this RouteDto route)
        {
            var result = new RouteVM(route.Id, route.Name, route.Description, route.Difficulty, route.LengthInMeters);

            result.Source = route.ToGeoJson();

            return result;
        }

        public static IEnumerable<GJSourceLine> ToGeoJson(this IEnumerable<RouteDto> routes)
        {
            var result = new List<GJSourceLine>(routes.Count());

            foreach (var route in routes)
            {
                result.Add(route.ToGeoJson());
            }

            return result;
        }

        public static IEnumerable<GJSourceLineResult> ToGeoJsonResult(this IEnumerable<RouteDto> routes)
        {
            var result = new List<GJSourceLineResult>(routes.Count());

            foreach (var route in routes)
            {
                result.Add(route.ToGeoJsonResult());
            }

            return result;
        }

        public static IEnumerable<RouteVM> ToGeoJsonVM(this IEnumerable<RouteDto> routes)
        {
            var result = new List<RouteVM>(routes.Count());

            foreach (var route in routes)
            {
                result.Add(route.ToGeoJsonVM());
            }

            return result;
        }
    }
}

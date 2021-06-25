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

        public static IEnumerable<RouteData> ToGeoJson(this RouteDto route)
        {
            var routeLine = new RouteData();

            var sortedPoints = route.Points
                .OrderBy(point => point.Order)
                .Select(point => new decimal[2] { point.Longitude, point.Latitude })
                .ToArray();

            routeLine.Geometry.Coordinates = sortedPoints;
            routeLine.Properties = new RouteProperties() { Title = route.Name, Difficulty = route.Difficulty };

            var routeStart = new RouteData();

            var firstPoint = route.Points
                .Where(point => point.Order == 1)
                .Select(point => new decimal[2] { point.Longitude, point.Latitude })
                .First();

            RouteGeometryPoint geometryPoint = new RouteGeometryPoint();
            geometryPoint.Coordinates = firstPoint;
            geometryPoint.Type = "Point";

            routeStart.Geometry = geometryPoint;
            routeStart.Properties = new RouteProperties() { Title = route.Name, Difficulty = route.Difficulty };

            var result = new RouteData[2] { routeLine, routeStart };

            return result;
        }
        public static GJRouteVM ToGeoJsonVM(this RouteDto route)
        {
            var result = new GJRouteVM(route.Id, route.Name, route.Description, route.Difficulty, route.LengthInMeters);

            result.Source.Data.Features = route.ToGeoJson();

            return result;
        }

        public static IEnumerable<RouteData> ToGeoJson(this IEnumerable<RouteDto> routes)
        {
            var result = new List<RouteData>(routes.Count() * 2);

            foreach (var route in routes)
            {
                foreach (var routeData in route.ToGeoJson())
                {
                    result.Add(routeData);
                }
            }

            return result;
        }

        public static IEnumerable<GJRouteVM> ToGeoJsonVM(this IEnumerable<RouteDto> routes)
        {
            var result = new List<GJRouteVM>(routes.Count());

            foreach (var route in routes)
            {
                result.Add(route.ToGeoJsonVM());
            }

            return result;
        }
 
    }
}

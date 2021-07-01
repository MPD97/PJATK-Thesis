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
                .ToArray();

            routeLine.Geometry.Coordinates = sortedPoints
                .Select(point => new decimal[2] { point.Longitude, point.Latitude })
                .ToArray();

            routeLine.Properties = new RouteLineProperties() { Type = RoutePointType.Line };

            var result = new RouteData[route.Points.Count() + 1];
            result[0] = routeLine;

            for (int i = 0; i < sortedPoints.Length; i++)
            {
                var routeData = new RouteData();
                routeData.Geometry = CreateRouteGeometryPoint(new decimal[2] { sortedPoints[i].Longitude, sortedPoints[i].Latitude });
                
                int idx = i + 1;
                if (i == 0)
                {
                    routeData.Properties = new RoutePointPropertiesWithRouteId(RoutePointType.Start, idx, sortedPoints[i].PointId, route.Id);
                }
                else if (i != sortedPoints.Length - 1)
                {
                    routeData.Properties = new RoutePointProperties(RoutePointType.End, idx, sortedPoints[i].PointId);
                }
                else
                {
                    routeData.Properties = new RoutePointProperties(RoutePointType.Other, idx, sortedPoints[i].PointId);
                }

                result[idx] = routeData;
            }

            return result;
        }
        private static RouteGeometryPoint CreateRouteGeometryPoint(decimal[] coordinates)
        {
            RouteGeometryPoint geometryPoint = new RouteGeometryPoint();
            geometryPoint.Coordinates = coordinates;
            geometryPoint.Type = "Point";

            return geometryPoint;
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

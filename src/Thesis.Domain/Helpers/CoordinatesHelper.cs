using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis.Domain.Static
{
    public static class CoordinatesHelper
    {
        private const double PIx = Math.PI;
        private const int EarthRadius = 6371000;

        /// <summary>
        /// Calculate distance between two points
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns>distance in meters</returns>
        public static double DistanceBetweenPlaces(double lat1, double lon1, double lat2, double lon2)
        {
            double sLat1 = Math.Sin(Radians(lat1));
            double sLat2 = Math.Sin(Radians(lat2));
            double cLat1 = Math.Cos(Radians(lat1));
            double cLat2 = Math.Cos(Radians(lat2));
            double cLon = Math.Cos(Radians(lon1) - Radians(lon2));

            double cosD = sLat1 * sLat2 + cLat1 * cLat2 * cLon;

            double d = Math.Acos(cosD);

            double dist = EarthRadius * d;

            return dist;
        }
        private static double Radians(double x)
        {
            return x * PIx / 180;
        }

        public static SquareBoundary GetBoundaries(decimal topLeftLat, decimal topLeftLon, decimal bottomRightLat, decimal bottomRightLon, decimal lat, decimal lon)
        {
            var sBoundary = new SquareBoundary(topLeftLat, topLeftLon, bottomRightLat, bottomRightLon);

            if (lat > sBoundary.TopLeftLat)
            {
                sBoundary.TopLeftLat = lat;
            }

            if (lon > sBoundary.BottomLeftLon)
            {
                sBoundary.BottomLeftLon = lon;
            }

            if (lon < sBoundary.TopLeftLon)
            {
                sBoundary.TopLeftLon = lon;
            }

            if (lat < sBoundary.BottomLeftLat)
            {
                sBoundary.BottomLeftLat = lat;
            }

            return sBoundary;
        }

        public struct SquareBoundary
        {
            public decimal TopLeftLat { get; set; }
            public decimal TopLeftLon { get; set; }

            public decimal BottomLeftLat { get; set; }
            public decimal BottomLeftLon { get; set; }

            public SquareBoundary(decimal topLeftLat, decimal topLeftLon, decimal bottomLeftLat, decimal bottomLeftLon)
            {
                TopLeftLat = topLeftLat;
                TopLeftLon = topLeftLon;
                BottomLeftLat = bottomLeftLat;
                BottomLeftLon = bottomLeftLon;
            }
        }
    }
}

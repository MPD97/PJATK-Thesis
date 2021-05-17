using System.Collections.Generic;
using Thesis.Domain.Commons;
using Thesis.Domain.Exceptions;

namespace Thesis.Domain.Entities
{
    public class Point : BaseEntity
    {
        private decimal latitude;
        private decimal longitude;

        public int Id { get; set; }
        public int RouteId { get; set; }
        public decimal Latitude
        {
            get => latitude; set
            {
                if (value < LATITUDE_MIN_VALUE)
                {
                    throw new DomainLayerException($"Property {nameof(Point)}.{nameof(Latitude)} cannot be less than {LATITUDE_MIN_VALUE}.");
                }
                if (value > LATITUDE_MAX_VALUE)
                {
                    throw new DomainLayerException($"Property {nameof(Point)}.{nameof(Latitude)} cannot be bigger than {LATITUDE_MAX_VALUE}.");
                }
                latitude = value;
            }
        }   // N/S
        public decimal Longitude { get => longitude; set {
                if (value < LONGITUDE_MIN_VALUE)
                {
                    throw new DomainLayerException($"Property {nameof(Point)}.{nameof(Longitude)} cannot be less than {LONGITUDE_MIN_VALUE}.");
                }
                if (value > LONGITUDE_MAX_VALUE)
                {
                    throw new DomainLayerException($"Property {nameof(Point)}.{nameof(Longitude)} cannot be bigger than {LONGITUDE_MAX_VALUE}.");
                }
                longitude = value;
            } }  // W/E
        public byte Order { get; set; }
        public byte Radius { get; set; }
        public virtual Route Route { get; set; }
        public virtual IList<CompletedPoints> CompletedPoints { get; set; } = new List<CompletedPoints>();

        public static readonly decimal LATITUDE_MIN_VALUE = -90M;
        public static readonly decimal LATITUDE_MAX_VALUE = 90M;

        public static readonly decimal LONGITUDE_MIN_VALUE = -180M;
        public static readonly decimal LONGITUDE_MAX_VALUE = 180M;

        public Point()
        {

        }

        public Point(decimal latitude, decimal longitude, byte order, byte radius)
        {
            Latitude = latitude;
            Longitude = longitude;
            Order = order;
            Radius = radius;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Domain.Commons;
using Thesis.Domain.Enums;
using Thesis.Domain.Exceptions;
using Thesis.Domain.Static;
using static Thesis.Domain.Static.CoordinatesHelper;

namespace Thesis.Domain.Entities
{
    public class Route : AuditableEntity
    {
        public int Id { get; protected set; }
        public string Name
        {
            get => name; protected set
            {

                if (value == null)
                {
                    throw new DomainLayerException($"Property {nameof(Route)}.{nameof(Name)} cannot be null.");
                }

                if (value.Length < NAME_MIN_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(Route)}.{nameof(Name)} cannot be smaller than {NAME_MIN_LENGTH} characters.");
                }

                if (value.Length > NAME_MAX_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(Route)}.{nameof(Name)} cannot be larger than {NAME_MAX_LENGTH} characters.");
                }
                name = value;
            }
        }
        public string Description { get; protected set; }
        public RouteDifficulty Difficulty { get; protected set; }
        public int LengthInMeters
        {
            get => lengthInMeters; set
            {
                if (value < 1)
                {
                    throw new DomainLayerException($"Property {nameof(Route)}.{nameof(LengthInMeters)} cannot be less than 1.");
                }
                lengthInMeters = value;
            }
        }
        public RouteStatus Status { get; protected set; } = RouteStatus.New;


        public decimal TopLeftLatitude
        {
            get => topLeftLatitude; set
            {
                if (value < LATITUDE_MIN_VALUE)
                {
                    throw new DomainLayerException($"Property {nameof(Point)}.{nameof(TopLeftLatitude)} cannot be less than {LATITUDE_MIN_VALUE}.");
                }
                if (value > LATITUDE_MAX_VALUE)
                {
                    throw new DomainLayerException($"Property {nameof(Point)}.{nameof(TopLeftLatitude)} cannot be bigger than {LATITUDE_MAX_VALUE}.");
                }
                topLeftLatitude = value;
            }
        }   // N/S
        public decimal TopLeftLongitude
        {
            get => topLeftLongitude; set
            {
                if (value < LONGITUDE_MIN_VALUE)
                {
                    throw new DomainLayerException($"Property {nameof(Point)}.{nameof(TopLeftLongitude)} cannot be less than {LONGITUDE_MIN_VALUE}.");
                }
                if (value > LONGITUDE_MAX_VALUE)
                {
                    throw new DomainLayerException($"Property {nameof(Point)}.{nameof(TopLeftLongitude)} cannot be bigger than {LONGITUDE_MAX_VALUE}.");
                }
                topLeftLongitude = value;
            }
        }  // W/E

        public decimal BottomLeftLatitude
        {
            get => bottomLeftLatitude; set
            {
                if (value < LATITUDE_MIN_VALUE)
                {
                    throw new DomainLayerException($"Property {nameof(Point)}.{nameof(BottomLeftLatitude)} cannot be less than {LATITUDE_MIN_VALUE}.");
                }
                if (value > LATITUDE_MAX_VALUE)
                {
                    throw new DomainLayerException($"Property {nameof(Point)}.{nameof(BottomLeftLatitude)} cannot be bigger than {LATITUDE_MAX_VALUE}.");
                }
                bottomLeftLatitude = value;
            }
        }   // N/S
        public decimal BottomLeftLongitude
        {
            get => bottomLeftLongitude; set
            {
                if (value < LONGITUDE_MIN_VALUE)
                {
                    throw new DomainLayerException($"Property {nameof(Point)}.{nameof(BottomLeftLongitude)} cannot be less than {LONGITUDE_MIN_VALUE}.");
                }
                if (value > LONGITUDE_MAX_VALUE)
                {
                    throw new DomainLayerException($"Property {nameof(Point)}.{nameof(BottomLeftLongitude)} cannot be bigger than {LONGITUDE_MAX_VALUE}.");
                }
                bottomLeftLongitude = value;
            }
        }  // W/E

        public virtual IList<Point> Points { get; protected set; } = new List<Point>();
        public virtual IList<Run> Runs { get; protected set; } = new List<Run>();

        public static readonly int NAME_MAX_LENGTH = 40;
        public static readonly int NAME_MIN_LENGTH = 4;

        public static readonly int DESCRIPTION_MAX_LENGTH = 500;

        public static readonly decimal LATITUDE_MIN_VALUE = -90M;
        public static readonly decimal LATITUDE_MAX_VALUE = 90M;

        public static readonly decimal LONGITUDE_MIN_VALUE = -180M;
        public static readonly decimal LONGITUDE_MAX_VALUE = 180M;

        private string name;
        private int lengthInMeters;

        private decimal topLeftLatitude;
        private decimal topLeftLongitude;

        private decimal bottomLeftLatitude;
        private decimal bottomLeftLongitude;

        public Route()
        {

        }

        public Route(string name, string description, RouteDifficulty difficulty, int userId)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty;

            Create(userId);
        }


        public void AddPoint(decimal latitude, decimal longitude, byte radius)
        {
            var order = (Points.Count + 1);
            if (order == byte.MaxValue)
            {
                throw new DomainLayerException("Exceeded max number of points!");
            }

            var point = new Point(latitude, longitude, (byte)order, radius);

            Points.Add(point);

            if (Points.Count > 1)
            {
                LengthInMeters += (int)DistanceBetweenPlaces((double)Points[^2].Latitude, (double)Points[^2].Longitude, (double)Points[^1].Latitude, (double)Points[^1].Longitude);
             
                SetBoundaries(GetBoundaries(TopLeftLatitude, TopLeftLongitude, BottomLeftLatitude, BottomLeftLongitude, latitude, longitude));
            }
            else
            {
                SetBoundaries(new SquareBoundary(latitude, longitude, latitude, longitude));
            }
        }
        public void ChangeDifficulty(RouteDifficulty difficulty, int userId)
        {
            if (Difficulty == difficulty)
            {
                throw new DomainLayerException("Cannot change difficulty to same difficulty");
            }

            Difficulty = difficulty;

            Update(userId);
        }

        public void ChangeStatus(RouteStatus status, int userId)
        {
            if (Status == status)
            {
                throw new DomainLayerException("Cannot change status to same status");
            }

            Status = status;

            Update(userId);
        }
        private void SetBoundaries(SquareBoundary boundary)
        {
            TopLeftLatitude = boundary.TopLeftLat;
            TopLeftLongitude = boundary.TopLeftLon;

            BottomLeftLatitude = boundary.BottomLeftLat;
            BottomLeftLongitude = boundary.BottomLeftLon;
        }
    }
}

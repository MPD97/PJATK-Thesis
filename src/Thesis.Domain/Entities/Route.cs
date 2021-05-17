using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Domain.Commons;
using Thesis.Domain.Enums;
using Thesis.Domain.Exceptions;
using Thesis.Domain.Static;

namespace Thesis.Domain.Entities
{
    public class Route : AuditableEntity
    {
        public int Id { get; set; }
        public string Name
        {
            get => name; set
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
        public string Description { get; set; }
        public RouteDifficulty Difficulty { get; set; }
        public int LengthKm
        {
            get => lengthKm; set
            {
                if (value < 1)
                {
                    throw new DomainLayerException($"Property {nameof(Route)}.{nameof(LengthKm)} cannot be less than 1.");
                }
                lengthKm = value;
            }
        }
        public RouteStatus Status { get; set; }
        public virtual IList<Point> Points { get; set; } = new List<Point>();
        public virtual IList<Run> Runs { get; set; } = new List<Run>();

        public static readonly int NAME_MAX_LENGTH = 40;
        public static readonly int NAME_MIN_LENGTH = 4;

        public static readonly int DESCRIPTION_MAX_LENGTH = 500;
        private string name;
        private int lengthKm;




        public Route()
        {

        }

        public Route(string name, string description, RouteDifficulty difficulty)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty;
        }

        public void AddPoint(decimal latitude, decimal longitude, byte radius)
        {
            var order = (Points.Count + 1);
            if (order == byte.MaxValue)
            {
                throw new Exception("Exceeded max number of points!");
            }

            var point = new Point(latitude, longitude, (byte)order, radius);

            Points.Add(point);

            if (Points.Count > 1)
            {
                LengthKm += (int)CoordinatesHelper.DistanceBetweenPlaces((double)Points[^2].Latitude, (double)Points[^2].Longitude, (double)Points[^1].Latitude, (double)Points[^1].Longitude);
            }
        }

    }
}

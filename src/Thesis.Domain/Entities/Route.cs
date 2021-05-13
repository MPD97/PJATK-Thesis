﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Domain.Commons;
using Thesis.Domain.Enums;
using Thesis.Domain.Exceptions;

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
        public virtual IList<Point> Points { get; set; }
        public virtual IList<Run> Runs { get; set; }

        public static readonly int NAME_MAX_LENGTH = 40;
        public static readonly int NAME_MIN_LENGTH = 4;

        public static readonly int DESCRIPTION_MAX_LENGTH = 500;
        private string name;
        private int lengthKm;
    }
}

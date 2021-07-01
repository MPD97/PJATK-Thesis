using System;
using Thesis.Domain.Commons;
using Thesis.Domain.Enums;
using Thesis.Domain.Exceptions;

namespace Thesis.Domain.Entities
{
    public class Achievement : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RouteId { get; set; }

        public AchievementType Type { get; set; }
        public DateTime Date { get; set; }
        public string Description
        {
            get => description; set
            {
                if (value.Length > DESCRIPTION_MAX_VALUE)
                {
                    throw new DomainLayerException($"Property {nameof(Achievement)}.{nameof(Description)} cannot be less than {DESCRIPTION_MAX_VALUE}.");
                }
                description = value;
            }
        }

        public static readonly int DESCRIPTION_MAX_VALUE = 500;

        private string description;
    }
    
}

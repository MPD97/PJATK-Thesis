using System;
using Thesis.Domain.Commons;
using Thesis.Domain.Enums;
using Thesis.Domain.Exceptions;

namespace Thesis.Domain.Entities
{
    public class Score : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public ScoreType Type { get; set; }
        public int? RouteId { get; set; }
        public Route Route { get; set; }

        public byte Amount
        {
            get => amount;
            set
            {
                if (value < AMOUNT_MIN_VALUE)
                {
                    throw new DomainLayerException($"Property {nameof(Score)}.{nameof(Amount)} cannot less than {AMOUNT_MIN_VALUE}.");
                }
                amount = value;
            }
        }
        public DateTime Date { get; set; }
        public string Description
        {
            get => this.description; set
            {
                if (value?.Length > DESCRIPTION_MAX_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(Score)}.{nameof(Description)} cannot be bigger than {DESCRIPTION_MAX_LENGTH}.");
                }
                description = value;
            }
        }

        public Score()
        {
        }

        public Score(int userId, ScoreType type, Route route, byte amount, DateTime date, string description)
        {
            UserId = userId;
            Type = type;
            Route = route;
            Amount = amount;
            Date = date;
            Description = description;
        }

        public static readonly int AMOUNT_MIN_VALUE = 1;
        public static readonly int DESCRIPTION_MAX_LENGTH = 80;

        private string description;
        private byte amount;
    }
}
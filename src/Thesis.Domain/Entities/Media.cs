using Thesis.Domain.Commons;
using Thesis.Domain.Enums;
using Thesis.Domain.Exceptions;

namespace Thesis.Domain.Entities
{
    public class Media : BaseEntity
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public string Value
        {
            get => this.value; set
            {
                if (value.Length > Value_MAX_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(Media)}.{nameof(Value)} cannot be bigger than {Value_MAX_LENGTH}.");
                }
                this.value = value;
            }
        }
        public MediaType MediaType { get; set; }


        public static readonly int Value_MAX_LENGTH = 50;
        private string value;
    }
}
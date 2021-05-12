using System.Collections.Generic;

namespace Thesis.Domain.Entities
{
    public class Point
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public decimal Latitude { get; set; }   // N/S
        public decimal Longitude { get; set; }  // W/E
        public byte Order { get; set; }
        public byte Radius { get; set; }
        public virtual Route Route { get; set; }
        public virtual IList<CompletedPoints> CompletedPoints { get; set; }
    }
}

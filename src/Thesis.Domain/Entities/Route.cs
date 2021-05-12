using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Domain.Commons;
using Thesis.Domain.Enums;

namespace Thesis.Domain.Entities
{
    public class Route : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RouteDifficulty Difficulty { get; set; }
        public int LengthKm { get; set; }
        public RouteStatus Status { get; set; }
        public virtual IList<Point> Points { get; set; }
        public virtual IList<Run> Runs { get; set; }
    }

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
    public class CompletedPoints
    {
        public int Id { get; set; }
        public int PointId { get; set; }
        public int RunId { get; set; }

        public virtual Point Point { get; set; }
        public virtual Run Run { get; set; }
    }
    public class Run
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int UserId { get; set; }

        public RunStatus Status { get; set; }
        public TimeSpan Time { get; set; }

        public virtual Route Route { get; set; }
        public virtual IList<CompletedPoints> CompletedPoints { get; set; }
    }
}

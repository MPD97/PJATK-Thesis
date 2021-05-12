using System;
using System.Collections.Generic;
using Thesis.Domain.Enums;

namespace Thesis.Domain.Entities
{
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

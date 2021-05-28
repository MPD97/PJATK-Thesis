using System;
using System.Collections.Generic;
using Thesis.Domain.Commons;
using Thesis.Domain.Enums;

namespace Thesis.Domain.Entities
{
    public class Run : BaseEntity
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int UserId { get; set; }

        public RunStatus Status { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual Route Route { get; set; }
        public virtual IList<CompletedPoints> CompletedPoints { get; set; }

        public Run()
        {

        }

        public Run(int userId, RunStatus status, DateTime startTime)
        {
            UserId = userId;
            Status = status;
            StartTime = startTime;
        }
    }
}

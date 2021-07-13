using System;
using System.Collections.Generic;
using Thesis.Domain.Commons;
using Thesis.Domain.Enums;
using System.Linq;
using Thesis.Domain.Exceptions;

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
        public virtual IList<CompletedPoint> CompletedPoints { get; set; } = new List<CompletedPoint>();

        public Run()
        {

        }

        public Run(int userId, Route route, RunStatus status, DateTime startTime)
        {
            UserId = userId;
            Route = route;
            Status = status;
            StartTime = startTime;
        }

        public CompletedPoint CompletePoint(Point pointToComplete, DateTime time)
        {
            var pointInRoute = Route.Points.FirstOrDefault(p => p.Id == pointToComplete.Id);
            if (pointInRoute is null)
                throw new DomainLayerException($"the point does not apply to the selected route");

            var existingPoint = CompletedPoints.FirstOrDefault(p => p.PointId == pointToComplete.Id);
            if (existingPoint is not null)
                throw new DomainLayerException($"You arleady completed this point");

            var completedPoint = new CompletedPoint(pointToComplete, time);
            
            CompletedPoints.Add(completedPoint);

            return completedPoint;
        }

        public Run CompleteRun(DateTime time)
        {
            var pointsInRoute = Route.Points;

            var allPointsCompleted = pointsInRoute.All(p => CompletedPoints.Any(cpp => cpp.Point.Id == p.Id));

            if (allPointsCompleted is false)
                throw new DomainLayerException($"Not all points have been completed");

            Status = RunStatus.Completed;
            EndTime = time;

            return this;
        }
    }
}

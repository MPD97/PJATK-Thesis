using System;
using Thesis.Domain.Commons;

namespace Thesis.Domain.Entities
{
    public class CompletedPoint : BaseEntity
    {
        public int Id { get; set; }
        public int PointId { get; set; }
        public int RunId { get; set; }

        public DateTime Time { get; set; }

        public virtual Point Point { get; set; }
        public virtual Run Run { get; set; }

        public CompletedPoint()
        {

        }

        public CompletedPoint(Point point, DateTime time)
        {
            Point = point;
            Time = time;
        }
    }
   
}

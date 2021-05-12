namespace Thesis.Domain.Entities
{
    public class CompletedPoints : BaseEntity
    {
        public int Id { get; set; }
        public int PointId { get; set; }
        public int RunId { get; set; }

        public virtual Point Point { get; set; }
        public virtual Run Run { get; set; }
    }
}

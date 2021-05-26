using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thesis.Domain.Entities;

namespace Thesis.Infrastructure.Presistance.Congiurations
{
    public class PointConfiguration : IEntityTypeConfiguration<Point>
    {
        public void Configure(EntityTypeBuilder<Point> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Latitude)
                .HasPrecision(11, 8);

            builder.Property(p => p.Longitude)
                .HasPrecision(11, 8);

            builder.HasMany(p => p.CompletedPoints)
                .WithOne(cp => cp.Point)
                .HasForeignKey(cp => cp.PointId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

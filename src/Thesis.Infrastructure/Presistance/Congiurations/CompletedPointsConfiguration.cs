using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thesis.Domain.Entities;

namespace Thesis.Infrastructure.Presistance.Congiurations
{
    public class CompletedPointsConfiguration : IEntityTypeConfiguration<CompletedPoint>
    {
        public void Configure(EntityTypeBuilder<CompletedPoint> builder)
        {
            builder.HasKey(cp => cp.Id);
        }
    }
}

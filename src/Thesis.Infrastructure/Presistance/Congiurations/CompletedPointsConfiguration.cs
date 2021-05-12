using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thesis.Domain.Entities;

namespace Thesis.Infrastructure.Presistance.Congiurations
{
    public class CompletedPointsConfiguration : IEntityTypeConfiguration<CompletedPoints>
    {
        public void Configure(EntityTypeBuilder<CompletedPoints> builder)
        {
            builder.HasKey(cp => cp.Id);
        }
    }
}

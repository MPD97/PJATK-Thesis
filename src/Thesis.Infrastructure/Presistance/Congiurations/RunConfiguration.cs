using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thesis.Domain.Entities;

namespace Thesis.Infrastructure.Presistance.Congiurations
{
    public class RunConfiguration : IEntityTypeConfiguration<Run>
    {
        public void Configure(EntityTypeBuilder<Run> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasMany(r => r.CompletedPoints)
                .WithOne(cp => cp.Run)
                .HasForeignKey(cp => cp.RunId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}

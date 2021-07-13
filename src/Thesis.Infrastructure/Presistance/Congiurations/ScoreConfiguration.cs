using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thesis.Domain.Entities;

namespace Thesis.Infrastructure.Presistance.Congiurations
{
    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder.Property(m => m.Description)
                .HasMaxLength(Score.DESCRIPTION_MAX_LENGTH);
        }
    }
}

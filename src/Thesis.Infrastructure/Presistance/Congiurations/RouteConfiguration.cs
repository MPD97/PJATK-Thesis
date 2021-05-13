using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thesis.Domain.Entities;

namespace Thesis.Infrastructure.Presistance.Congiurations
{
    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .HasMaxLength(Route.NAME_MAX_LENGTH)
                .IsRequired();

            builder.Property(r => r.Description)
                .HasMaxLength(Route.DESCRIPTION_MAX_LENGTH)
                .IsRequired(false);

            builder.HasMany(r => r.Points)
                .WithOne(p => p.Route)
                .HasForeignKey(p => p.RouteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Runs)
                .WithOne(r => r.Route)
                .HasForeignKey(r => r.RouteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

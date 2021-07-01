using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thesis.Domain.Entities;

namespace Thesis.Infrastructure.Presistance.Congiurations
{
    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(r => r.Id)
                .IsClustered(false);

            builder.HasIndex(r => r.Id)
                .IsUnique();

            builder.Property(r => r.Name)
                .HasMaxLength(Route.NAME_MAX_LENGTH)
                .IsRequired();

            builder.HasIndex(r => r.Name)
                .IsUnique();

            builder.Property(r => r.Description)
                .HasMaxLength(Route.DESCRIPTION_MAX_LENGTH)
                .IsRequired(false);

            builder.Property(p => p.TopLeftLatitude)
                .HasPrecision(11, 8);

            builder.Property(p => p.TopLeftLongitude)
                .HasPrecision(11, 8);

            builder.Property(p => p.BottomLeftLatitude)
                .HasPrecision(11, 8);

            builder.Property(p => p.BottomLeftLongitude)
                .HasPrecision(11, 8);

            builder.HasIndex(p => new { p.TopLeftLatitude, p.TopLeftLongitude, p.BottomLeftLatitude, p.BottomLeftLongitude })
                .IsClustered();

            builder.HasMany(r => r.Points)
                .WithOne(p => p.Route)
                .HasForeignKey(p => p.RouteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Runs)
                .WithOne(r => r.Route)
                .HasForeignKey(r => r.RouteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Medias)
                .WithOne(r => r.Route)
                .HasForeignKey(r => r.RouteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

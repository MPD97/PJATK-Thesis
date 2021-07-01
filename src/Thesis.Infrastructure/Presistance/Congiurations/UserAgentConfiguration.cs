using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thesis.Domain.Entities;
using Thesis.Infrastructure.Identity;

namespace Thesis.Infrastructure.Presistance.Congiurations
{
    public class UserAgentConfiguration : IEntityTypeConfiguration<UserAgent>
    {
        public void Configure(EntityTypeBuilder<UserAgent> builder)
        {
            builder.Property(ua => ua.Raw)
                .HasMaxLength(UserAgent.RAW_MAX_LENGTH);

            builder.Property(ua => ua.BrowserFamily)
                .HasMaxLength(UserAgent.BROWSER_FAMILY_MAX_LENGTH);

            builder.Property(ua => ua.BrowserMajorVersion)
                .HasMaxLength(UserAgent.BROWSER_MAJOR_VERSION_MAX_LENGTH);

            builder.Property(ua => ua.BrowserMinorVersion)
                .HasMaxLength(UserAgent.BROWSER_MINOR_VERSION_MAX_LENGTH);

            builder.Property(ua => ua.OSFamily)
                .HasMaxLength(UserAgent.OS_FAMILY_MAX_LENGTH);

            builder.Property(ua => ua.OSMajorVersion)
                .HasMaxLength(UserAgent.OS_MAJOR_VERSION_MAX_LENGTH);

            builder.Property(ua => ua.OSMinorVersion)
                .HasMaxLength(UserAgent.OS_MINOR_VERSION_MAX_LENGTH);

            builder.Property(ua => ua.DeviceFamily)
                .HasMaxLength(UserAgent.DEVICE_FAMILY_MAX_LENGTH);
        }
    }
}

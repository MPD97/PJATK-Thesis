using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Infrastructure.Identity;

namespace Thesis.Infrastructure.Presistance.Congiurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasMany(u => u.CreatedRoutes)
                .WithOne()
                .HasForeignKey(r => r.CreatedBy)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.ModifiedRoutes)
                .WithOne()
                .HasForeignKey(r => r.LastModifiedBy)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Runs)
                .WithOne()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

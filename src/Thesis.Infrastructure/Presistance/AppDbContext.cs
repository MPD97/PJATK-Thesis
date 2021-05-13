using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thesis.Infrastructure.Identity;
using Thesis.Infrastructure.Presistance.Abstract;

namespace Thesis.Infrastructure.Presistance
{
    public class AppDbContext : KeyApiAuthorizationDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<AppUser>(entity => { entity.ToTable(name: "User"); });
        //    modelBuilder.Entity<AppRole>(entity => { entity.ToTable(name: "Role"); });

        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        //}
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Domain.Entities;

namespace Thesis.Infrastructure.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public virtual IList<Route> CreatedRoutes { get; set; }
        public virtual IList<Route> ModifiedRoutes { get; set; }
        public virtual IList<Run> Runs { get; set; }

        public AppUser()
        {
        }

        public AppUser(string email)
        {
            UserName = email;
            NormalizedUserName = email.ToUpper();
            Email = email;
            NormalizedEmail = email.ToUpper();
        }
    }
}

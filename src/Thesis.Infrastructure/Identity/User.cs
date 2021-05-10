using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis.Infrastructure.Identity
{
    public class AppUser : IdentityUser<int>
    {
    }
    public class AppRole : IdentityRole<int>
    {
    }
}

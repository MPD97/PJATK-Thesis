using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Infrastructure.Identity;
using Thesis.Infrastructure.Presistance;

namespace Thesis.Infrastructure.Services
{
    public class DataSeederService : IDataSeederService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DataSeederService(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task CreateTestRoute()
        {
            var testUser = new AppUser()
            {

            }
        }

        public Task CreateTestUser()
        {
            throw new NotImplementedException();
        }
    }
}

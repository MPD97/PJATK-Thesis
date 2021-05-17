using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;
using Thesis.Domain.Enums;
using Thesis.Infrastructure.Identity;
using Thesis.Infrastructure.Presistance;

namespace Thesis.Infrastructure.Services
{
    public class DataSeederService : IDataSeederService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        private AppUser _testUser = new AppUser("test@test.pl");

        public DataSeederService(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task CreateTestRoute()
        {
            var existingUser = await _userManager.FindByEmailAsync(_testUser.Email);
            if (existingUser == null)
            {
                throw new Exception($"Test user not exist. Run {nameof(CreateTestUser)} method first.");
            }
            var route = new Route("Trasa testowa", "Opis trasy testowej", RouteDifficulty.Green, existingUser.Id);
            route.AddPoint(52.183145708512654M, 21.432822680367927M, 10);
            route.AddPoint(52.183057202090545M, 21.436503590733746M, 10);
            route.AddPoint(52.182813808521466M, 21.43924622982985M, 10);
            route.AddPoint(52.1823712713466M, 21.44213321835206M, 10);
            route.AddPoint(52.182592540484485M, 21.445381080439542M, 10);
            route.AddPoint(52.18341122672211M, 21.45151593104924M, 10);

        }

        public async Task CreateTestUser()
        {
            var existingUser = await _userManager.FindByEmailAsync(_testUser.Email);
            if (existingUser == null)
            {
                await _userManager.CreateAsync(_testUser, "1qaz@WSX");
            }
        }
    }
}

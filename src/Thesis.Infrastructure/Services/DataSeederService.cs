using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<Route> _routeRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDateTime _dateTime;

        private AppUser _testUser;
        private AppUser _testUser2;

        public DataSeederService(IRepository<Route> routeRepository, UserManager<AppUser> userManager, IDateTime dateTime)
        {
            _routeRepository = routeRepository;
            _userManager = userManager;
            _dateTime = dateTime;

            _testUser = new AppUser("test@test.pl", "Testowik1", dateTime.Now);
            _testUser2 = new AppUser("test2@test.pl", "Testowik2", dateTime.Now);
        }

        public async Task CreateTestRoute()
        {
            var existingUser = await _userManager.FindByEmailAsync(_testUser.Email);
            if (existingUser == null)
            {
                throw new Exception($"Test user not exist. Run {nameof(CreateTestUser)} method first.");
            }

            if (await _routeRepository.FindBy(r => r.Name == "Trasa testowa").FirstOrDefaultAsync() == null)
            {
                var route = new Route("Trasa testowa", "Opis trasy testowej", RouteDifficulty.Green, RouteActivityKind.Walking, existingUser.Id);
                route.AddPoint(52.183145708512654M, 21.432822680367927M, 10);
                route.AddPoint(52.183057202090545M, 21.436503590733746M, 10);
                route.AddPoint(52.182813808521466M, 21.43924622982985M, 10);
                route.AddPoint(52.1823712713466M, 21.44213321835206M, 10);
                route.AddPoint(52.182592540484485M, 21.445381080439542M, 10);
                route.AddPoint(52.18341122672211M, 21.45151593104924M, 10);

                route.ChangeStatus(RouteStatus.Accepted, existingUser.Id);

                await _routeRepository.AddAsync(route);
                await _routeRepository.SaveChangesAsync();
            }

            if (await _routeRepository.FindBy(r => r.Name == "Trasa testowa 2").FirstOrDefaultAsync() == null)
            {
                var route = new Route("Trasa testowa 2", "Opis trasy testowej", RouteDifficulty.Blue, RouteActivityKind.Running, existingUser.Id);
                route.AddPoint(52.17938994006697M, 21.426149905087946M, 10);
                route.AddPoint(52.178574157889344M, 21.428166926121726M, 10);
                route.AddPoint(52.17782415139548M, 21.428960859932896M, 10);
                route.AddPoint(52.177337298305744M, 21.43029123550837M, 10);
                route.AddPoint(52.1772846652201M, 21.42788797640429M, 10);
                route.AddPoint(52.1773241400402M, 21.423296034901853M, 10);

                route.AddPoint(52.1772846652201M, 21.42720133094598M, 10);
                route.AddPoint(52.1773241400402M, 21.43031269317894M, 10);
                route.AddPoint(52.177929416227755M, 21.428767740897747M, 10);
                route.AddPoint(52.17862678944912M, 21.428145468451156M, 10);
                route.AddPoint(52.179482043566324M, 21.426042616735085M, 10);

                route.ChangeStatus(RouteStatus.Accepted, existingUser.Id);

                await _routeRepository.AddAsync(route);
                await _routeRepository.SaveChangesAsync();
            }
        }

        public async Task CreateTestUser()
        {
            var existingUser = await _userManager.FindByEmailAsync(_testUser.Email);
            if (existingUser == null)
            {
                await _userManager.CreateAsync(_testUser, "1qaz@WSX");
            }

            var existingUser2 = await _userManager.FindByEmailAsync(_testUser2.Email);
            if (existingUser2 == null)
            {
                await _userManager.CreateAsync(_testUser2, "1qaz@WSX");
            }
        }
    }
}

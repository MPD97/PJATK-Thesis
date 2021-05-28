using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;
using Thesis.Domain.Enums;

namespace Thesis.Infrastructure.Services
{
    public class DbRunService : IRunService
    {

        private readonly IRepository<Run> _repository;
        private readonly IDateTime _date;

        public DbRunService(IRepository<Run> repository, IDateTime date)
        {
            _repository = repository;
            _date = date;
        }

        public Task<Run> CompleteRun(int routeId, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Run> GetActiveRun(int routeId, int userId)
        {
            var run = await _repository
                .GetAll()
                .AsNoTracking()
                .Where(r => r.RouteId == routeId)
                .Where(r => r.UserId == userId)
                .Where(r => r.Status == RunStatus.InProgress)
                .FirstOrDefaultAsync();

            return run;
        }

        public async Task<Run> GetActiveRun(int userId)
        {
            var run = await _repository
                .FindBy(r => r.UserId == userId)
                .AsNoTracking()
                .Where(r => r.Status == RunStatus.InProgress)
                .FirstOrDefaultAsync();

            return run;
        }

        public async Task<Run> GetRun(int runId)
        {
            var run = await _repository
                .GetAll()
                .AsNoTracking()
                .Where(r => r.Id == runId)
                .FirstOrDefaultAsync();

            return run;
        }

        public async Task<IReadOnlyList<Run>> GetUserRuns(int userId)
        {
            var runs = await _repository
                .GetAll()
                .AsNoTracking()
                .Where(r => r.UserId == userId)
                .Where(r => r.Status == RunStatus.Completed)
                .ToListAsync();

            return runs;
        }
    }
}

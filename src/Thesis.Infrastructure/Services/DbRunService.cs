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

        private readonly IRepository<Run> _runRepository;
        private readonly IRepository<CompletedPoint> _completedPointsRepository;
        private readonly IDateTime _date;

        public DbRunService(IRepository<Run> runRepository, IDateTime date, IRepository<CompletedPoint> completedPointsRepository)
        {
            _runRepository = runRepository;
            _date = date;
            _completedPointsRepository = completedPointsRepository;
        }

        public async Task<Run> CancelRun(Run run)
        {
            run.Status = RunStatus.Canceled;
            run.EndTime = null;

            await _runRepository.UpdateAsync(run);
            await _runRepository.SaveChangesAsync();

            return run;
        }

        public async Task<CompletedPoint> CompletePoint(Run run, Point point)
        {
            var completedPoint = run.CompletePoint(point, _date.Now);

            await _completedPointsRepository.AddAsync(completedPoint);

            return completedPoint;
        }

        public Task<Run> CompleteRun(int routeId, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Run> GetActiveRun(int routeId, int userId)
        {
            var run = await _runRepository
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
            var run = await _runRepository
                .FindBy(r => r.UserId == userId)
                .AsNoTracking()
                .Where(r => r.Status == RunStatus.InProgress)
                .FirstOrDefaultAsync();

            return run;
        }

        public async Task<Run> GetRun(int runId)
        {
            var run = await _runRepository
                .GetAll()
                .AsNoTracking()
                .Where(r => r.Id == runId)
                .FirstOrDefaultAsync();

            return run;
        }

        public async Task<IReadOnlyList<Run>> GetUserRuns(int userId)
        {
            var runs = await _runRepository
                .GetAll()
                .AsNoTracking()
                .Where(r => r.UserId == userId)
                .Where(r => r.Status == RunStatus.Completed)
                .ToListAsync();

            return runs;
        }
    }
}

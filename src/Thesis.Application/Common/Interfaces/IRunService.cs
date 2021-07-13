using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Domain.Entities;

namespace Thesis.Application.Common.Interfaces
{
    public interface IRunService
    {
        Task<CompletedPoint> CompletePoint(Run run, Point point);
        Task<Run> GetRunNoTracking(int runId);
        Task<IReadOnlyList<Run>> GetUserRunsNoTracking(int userId);

        Task<Run> GetActiveRunNoTracking(int routeId, int userId);

        Task<Run> GetActiveRunNoTracking(int userId);

        Task<Run> GetActiveRun(int userId);

        Task<Run> CancelRun(Run run);

        Task<Run> CompleteRun(Run run);

        Task SaveChangesAsync();
    }
}

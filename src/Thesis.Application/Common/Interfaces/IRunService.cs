using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Domain.Entities;

namespace Thesis.Application.Common.Interfaces
{
    public interface IRunService
    {
        Task<CompletedPoint> CompletePoint(Run run, Point point);
        Task<Run> GetRun(int runId);
        Task<IReadOnlyList<Run>> GetUserRuns(int userId);

        Task<Run> GetActiveRun(int routeId, int userId);

        Task<Run> GetActiveRun(int userId);

        Task<Run> CancelRun(Run run);

        Task<Run> CompleteRun(int routeId, int userId);
    }
}

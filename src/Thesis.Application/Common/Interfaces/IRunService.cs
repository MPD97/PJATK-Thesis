using System.Collections.Generic;
using System.Threading.Tasks;
using Thesis.Domain.Entities;

namespace Thesis.Application.Common.Interfaces
{
    public interface IRunService
    {
        Task<Run> GetRun(int runId);
        Task<IReadOnlyList<Run>> GetUserRuns(int userId);

        Task<Run> GetActiveRun(int routeId, int userId);

        Task<Run> CreateRun(int routeId, int userId);

        Task<Run> CompleteRun(int routeId, int userId);
    }
}

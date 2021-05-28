using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;

namespace Thesis.Infrastructure.Services
{
    public class DbRunService : IRunService
    {

        private readonly IRepository<Run> _repository;

        public Task<Run> CompleteRun(int routeId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Run> CreateRun(int routeId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Run> GetActiveRun(int routeId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Run> GetRun(int runId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Run>> GetUserRuns(int userId)
        {
            throw new NotImplementedException();
        }
    }
}

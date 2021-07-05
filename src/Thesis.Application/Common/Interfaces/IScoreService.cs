using System;
using System.Threading.Tasks;
using Thesis.Domain.Entities;
using Thesis.Domain.Enums;

namespace Thesis.Application.Common.Interfaces
{
    public interface IScoreService
    {
        Task IncrementScoreRouteAdded(int userId, DateTime date, Route route);
        Task IncrementScoreRouteCompleted(int userId, DateTime date, Route route);
        Task IncrementScoreRouteCompletedEndOfMonth(int userId, DateTime date, DateTime forDate, Route route, RoutePlace place);
        Task IncrementScoreRouteComment(int userId, DateTime date, Route route, bool photo = false);
    }
}

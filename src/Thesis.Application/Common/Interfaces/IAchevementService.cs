using System;
using System.Threading.Tasks;
using Thesis.Domain.Entities;
using Thesis.Domain.Enums;

namespace Thesis.Application.Common.Interfaces
{
    public interface IAchevementService
    {
        Task RewardBestPlace(int userId, DateTime date, DateTime forDate, Route route, RouteAchievementPlace place);
        Task RewardEnergyGoal(int userId, DateTime date, RouteAchievementType type);
    }
}

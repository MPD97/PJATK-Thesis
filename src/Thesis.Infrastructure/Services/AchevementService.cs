using System;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;
using Thesis.Domain.Enums;

namespace Thesis.Infrastructure.Services
{
    public class AchevementService : IAchevementService
    {
        private readonly IRepository<Achievement> _repository;

        public AchevementService(IRepository<Achievement> repository)
        {
            _repository = repository;
        }

        public async Task RewardBestPlace(int userId, DateTime date, DateTime forDate, Route route, RouteAchievementPlace place)
        {
            var achievementType = AchievementType.ThirdPlace;
            var placeText = string.Empty;
            switch (place)
            {
                case RouteAchievementPlace.First:
                    achievementType = AchievementType.FirstPlace;
                    placeText = "Pierwsze";
                    break;
                case RouteAchievementPlace.Second:
                    achievementType = AchievementType.SecondPlace;
                    placeText = "Drugie";
                    break;
                case RouteAchievementPlace.Third:
                    achievementType = AchievementType.ThirdPlace;
                    placeText = "Trzecie";
                    break;
                default:
                    throw new NotImplementedException($"Unknown {nameof(place)}");
            }

            var achievement = new Achievement(userId, achievementType, date, $"{placeText} miejsce na trasie \"{route.Name}\" w miesiącu {forDate.ToString("MMMM")} {forDate.ToString("yyyy")}");
            
            await _repository.AddAsync(achievement);
            await _repository.SaveChangesAsync();
        }

        public async Task RewardEnergyGoal(int userId, DateTime date, RouteAchievementType type)
        {
            var achievementType = AchievementType.BronzeEnergyOrder;
            var placeText = string.Empty;
            switch (type)
            {
                case RouteAchievementType.Master:
                    achievementType = AchievementType.MasterEnergyOrder;
                    placeText = "Mistrz energii";
                    break;
                case RouteAchievementType.Gold:
                    achievementType = AchievementType.GoldEnergyOrder;
                    placeText = "Złoty medal energii";
                    break;
                case RouteAchievementType.Silver:
                    achievementType = AchievementType.SilverEnergyOrder;
                    placeText = "Srebrny medal energii";
                    break;
                case RouteAchievementType.Bronze:
                    achievementType = AchievementType.BronzeEnergyOrder;
                    placeText = "Brązowy medal energii";
                    break;
                default:
                    break;
            }

            var achievement = new Achievement(userId, achievementType, date, placeText);

            await _repository.AddAsync(achievement);
            await _repository.SaveChangesAsync();
        }
    }
}

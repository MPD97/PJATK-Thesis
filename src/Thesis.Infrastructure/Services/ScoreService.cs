using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;
using Thesis.Domain.Enums;

namespace Thesis.Infrastructure.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IRepository<Score> _repository;

        public ScoreService(IRepository<Score> repository)
        {
            _repository = repository;
        }

        public async Task IncrementScoreRouteAdded(int userId, DateTime date, Route route)
        {
            var score = new Score(userId, ScoreType.RouteAdded, route, 5, date, $"Dodanie nowej trasy: {route.Name}");

            await _repository.AddAsync(score);
            await _repository.SaveChangesAsync();
        }

        public async Task IncrementScoreRouteComment(int userId, DateTime date, Route route, bool photo = false)
        {
            var scoreType = ScoreType.Comented;
            byte points = 2;
            string withPhoto = string.Empty;
            if (photo)
            {
                scoreType = ScoreType.ComentedWithImg;
                points = 5;
                withPhoto = " wraz ze zdjęciem";
            }

            var score = await _repository
                .FindBy(s => s.UserId == userId)
                .Where(s => s.RouteId == route.Id)
                .Where(s => s.Type == scoreType)
                .FirstOrDefaultAsync();

            if (score is not null)
                return;

            score = new Score(userId, scoreType, route, points, date, $"Dodanie komentarza pod trasą: {route.Name}{withPhoto}");

            await _repository.AddAsync(score);
            await _repository.SaveChangesAsync();
        }

        public async Task IncrementScoreRouteCompleted(int userId, DateTime date, Route route)
        {
            var score = new Score(userId, ScoreType.RouteCompleted, route, 5, date, $"Ukończenie trasy: {route.Name}");

            await _repository.AddAsync(score);
            await _repository.SaveChangesAsync();
        }

        public async Task IncrementScoreRouteCompletedEndOfMonth(int userId, DateTime date, DateTime forDate, Route route, RoutePlace place)
        {
            var scoreType = ScoreType.TopTen;
            byte points = 10;
            switch (place)
            {
                case RoutePlace.First:
                    scoreType = ScoreType.TopOne;
                    points = 35;
                    break;
                case RoutePlace.Second:
                    scoreType = ScoreType.TopTwo;
                    points = 25;
                    break;
                case RoutePlace.Third:
                    scoreType = ScoreType.TopThree;
                    points = 15;
                    break;
                case RoutePlace.TopTen:
                    scoreType = ScoreType.TopTen;
                    points = 10;
                    break;
                default:
                    throw new NotImplementedException($"Unknown {nameof(place)}");
            }

            var score = new Score(userId, scoreType, route, points, date, $"Za ukończenie trasy: {route.Name} będąc top {(int)place} w miesiącu {forDate.ToString("mmmm")} {forDate.ToString("yyyy")}");
            
            await _repository.AddAsync(score);
            await _repository.SaveChangesAsync();
        }
    }
}

using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Application.Common.Configurations;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Static;

namespace Thesis.Application.Common.Routes.Commands.CreateRun
{
    public class CreateRunCommandValidator : AbstractValidator<CreateRunCommand>
    {
        private readonly IRunService _runService;
        private readonly IPointService _pointService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ApplicationConfiguration _configuration;

        public CreateRunCommandValidator(IRunService runService, IPointService pointService, ICurrentUserService currentUserService, IOptions<ApplicationConfiguration> configuration)
        {
            _runService = runService;
            _pointService = pointService;
            _currentUserService = currentUserService;
            _configuration = configuration.Value;

            RuleFor(v => v.RouteId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("RouteId must be greater than or equal 1");

            RuleFor(v => v.Accuracy)
                .GreaterThanOrEqualTo(0)
                .WithMessage($"Accuracy cannot be negative.");

            RuleFor(v => v.Latitude)
               .Must(BeValidLatitude)
               .WithMessage($"Given Latitude is not in range.");

            RuleFor(v => v.Longitude)
               .Must(BeValidLongitude)
               .WithMessage($"Given Longitude is not in range.");

            RuleFor(v => new { v.RouteId, v.Latitude, v.Longitude })
               .MustAsync(async (x, cancellation) => await BeInValidDistance(x.RouteId, x.Latitude, x.Longitude, cancellation))
               .WithMessage($"You are too far from starting point. Max distance is {_configuration.MaxDistance} meters. Come a little bit closer and start again.");

            RuleFor(v =>v.Accuracy)
               .Must(BeInGoodAccuracy)
               .WithMessage($"Your GPS signal is to weak. Accuracy must be less or equal than {_configuration.MaxAccuracy} meters. Try again later.");
        }

        private bool BeValidLatitude(decimal latitude)
        {
            return latitude <= 90.0M && latitude >= -90.0M;
        }

        private bool BeValidLongitude(decimal longitude)
        {
            return longitude <= 180.0M && longitude >= -180.0M;
        }

        private async Task<bool> BeInValidDistance(int routeId, decimal latitude, decimal longitude, CancellationToken cancellation)
        {
            var point = await _pointService.GetPointNoTracking(routeId, 1);
            if (point is null)
                return false;

            var distance = (int)CoordinatesHelper.DistanceBetweenPlaces((double)latitude, (double)longitude, (double)point.Latitude, (double)point.Longitude);
            if (distance > _configuration.MaxDistance)
                return false;

            return true;
        }

        private bool BeInGoodAccuracy(int accuracy)
        {
            return accuracy <= _configuration.MaxAccuracy;
        }
    }
}

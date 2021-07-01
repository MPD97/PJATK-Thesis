using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Application.Common.Routes.Commands.CreateRun;
using Thesis.Domain.Entities;

namespace Thesis.Application.Common.Routes.Commands.ReachPoint
{
    public class ReachPointCommand : IRequest<RunDto>
    {
        public int RunId { get; set; }
        public int PointId { get; init; }
        public decimal Latitude { get; init; }
        public decimal Longitude { get; init; }
        public int Accuracy { get; init; }

        public ReachPointCommand()
        {

        }

        public ReachPointCommand(int pointId, decimal latitude, decimal longitude, int accuracy)
        {
            PointId = pointId;
            Latitude = latitude;
            Longitude = longitude;
            Accuracy = accuracy;
        }
    }

    public class ReachPointCommandHandler : IRequestHandler<ReachPointCommand, RunDto>
    {
        private readonly IMapper _mapper;
        private readonly IRunService _runService;
        private readonly IPointService _pointService;
        private readonly ICurrentUserService _currentUserService;

        public ReachPointCommandHandler(IMapper mapper, IRunService runService, IPointService pointService, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _runService = runService;
            _pointService = pointService;
            _currentUserService = currentUserService;
        }

        public async Task<RunDto> Handle(ReachPointCommand request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUserService.UserId);

            var activeRun = await _runService.GetActiveRun(userId);
            if (activeRun is null)
                return null;

            var pointToComplete = await _pointService.GetPoint(request.PointId);
            if (pointToComplete is null)
                return null;

            CompletedPoint completedPoint = null;
            if (pointToComplete.NextPoint is null)
            {
                completedPoint = await _runService.CompletePoint(activeRun, pointToComplete);
                await _runService.CompleteRun(activeRun);

                await _runService.SaveChangesAsync();
            }
            else
            {
                completedPoint = await _runService.CompletePoint(activeRun, pointToComplete);

                await _runService.SaveChangesAsync();
            }


            var result = _mapper.Map<RunDto>(activeRun);

            return result;
        }
    }
}

﻿using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Thesis.Application.Common.Mappings;
using Thesis.Application.Common.Routes.Queries.GetRoutes;
using Thesis.Domain.Entities;
using Thesis.Domain.Enums;

namespace Thesis.Application.Common.Routes.Commands.CreateRun
{
    public class RunDto : IMapFrom<Run>
    {
        public int Id { get; set; }
        public RunStatus Status { get; set; }
        public DateTime StartTime { get; set; }

        public PointDto NextPoint { get; set; }
        public PointDto CompletedPoint { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Run, RunDto>()
                .ForMember(run => run.CompletedPoint, m => m.MapFrom(src => src.CompletedPoints.Where(cp => cp.Point.Order == src.CompletedPoints.Max(w => w.Point.Order)).FirstOrDefault().Point))
                .ForMember(run => run.NextPoint, m => m.MapFrom(src => src.CompletedPoints.Where(cp => cp.Point.Order == src.CompletedPoints.Max(w => w.Point.Order)).FirstOrDefault().Point.NextPoint));
        }
    }
   
}

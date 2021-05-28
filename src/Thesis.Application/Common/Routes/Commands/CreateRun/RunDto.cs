
using AutoMapper;
using System;
using Thesis.Application.Common.Mappings;
using Thesis.Domain.Entities;
using Thesis.Domain.Enums;

namespace Thesis.Application.Common.Routes.Commands.CreateRun
{
    public class RunDto : IMapFrom<Run>
    {
        public int Id { get; set; }
        public RunStatus Status { get; set; }
        public DateTime StartTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Run, RunDto>();
        }
    }
}

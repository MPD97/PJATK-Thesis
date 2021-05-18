using AutoMapper;
using Thesis.Application.Common.Mappings;
using Thesis.Domain.Entities;
using Thesis.Domain.Enums;

namespace Thesis.Application.Common.Routes.Queries
{
    public class RouteDto : IMapFrom<Route>
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public RouteDifficulty Difficulty { get; protected set; }
        public int LengthInMeters { get; protected set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Route, RouteDto>();
        }
    }
}

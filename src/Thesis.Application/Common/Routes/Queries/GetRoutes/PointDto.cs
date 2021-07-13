using AutoMapper;
using Thesis.Application.Common.Mappings;
using Thesis.Domain.Entities;

namespace Thesis.Application.Common.Routes.Queries.GetRoutes
{
    public class PointDto : IMapFrom<Point>
    {
        public int PointId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public byte Order { get; set; }
        public byte Radius { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Point, PointDto>()
                .ForMember(p => p.PointId, m => m.MapFrom(src => src.Id));
        }
    }
}

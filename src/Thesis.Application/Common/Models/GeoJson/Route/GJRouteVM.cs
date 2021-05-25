using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Models.GeoJson.Line;
using Thesis.Domain.Enums;

namespace Thesis.Application.Common.Models.GeoJson.Route
{
    public class GJRouteVM
    {
        public int RouteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RouteDifficulty Difficulty { get; set; }
        public int LengthInMeters { get; set; }
        public RouteSource Source { get; set; } = new();

        public GJRouteVM()
        {

        }
        public GJRouteVM(int routeId, string name, string description, RouteDifficulty difficulty, int lengthInMeters)
        {
            RouteId = routeId;
            Name = name;
            Description = description;
            Difficulty = difficulty;
            LengthInMeters = lengthInMeters;
        }
    }
}

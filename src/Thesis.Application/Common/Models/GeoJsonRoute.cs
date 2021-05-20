using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis.Application.Common.Models
{
    public abstract class GeoJsonBase
    {
        public string Type { get; set; } = "geojson";
        public GeoJsonData Data { get; set; } = new ();

        public class GeoJsonData
        {
            public string Type { get; set; } = "Feature";
            public GeoJsonProperties Properties { get; set; } = new ();
            public GeoJsonGeometry Geometry { get; set; } = new ();


            public class GeoJsonProperties
            {

            }

            public class GeoJsonGeometry
            {
                public string Type { get; set; } = "LineString";

                public decimal[,] GeoJsonCoordinates { get; set; }
            }
        }
    }
}

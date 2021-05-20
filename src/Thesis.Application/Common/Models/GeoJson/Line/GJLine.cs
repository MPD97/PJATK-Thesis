using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Models.GeoJson.Base;

namespace Thesis.Application.Common.Models.GeoJson.Line
{
    public class GJLine : GJBase
    {
        public new GJDataLine Data { get; set; } = new();
    }
}

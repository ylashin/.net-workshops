using MarsRoversApp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.Parsers
{
    public static class PlateauSpecsParser
    {
        public static PlateauSpecs Parse(string plateauSpecsText)
        {
            var plateauSpecs = PlateauSpecs.Build(-1,-1);
            return plateauSpecs;
        }
    }
}

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
            if (string.IsNullOrWhiteSpace(plateauSpecsText))
                throw new ParseException(ApplicationMessages.InvalidHeaderNotTwoParts);

            var parts = plateauSpecsText.Split(' ');
            if (parts.Length!=2)
                throw new ParseException(ApplicationMessages.InvalidHeaderNotTwoParts);

            int part1,part2;
            if (int.TryParse(parts[0],out part1) == false)
                throw new ParseException(ApplicationMessages.InvalidHeaderNonNumberDimensions);
            if (int.TryParse(parts[1], out part2) == false)
                throw new ParseException(ApplicationMessages.InvalidHeaderNonNumberDimensions);
            
            return PlateauSpecs.Build(part1,part2);
        }
    }
}

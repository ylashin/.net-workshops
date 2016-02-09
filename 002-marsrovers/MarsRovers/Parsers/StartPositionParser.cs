using MarsRoversApp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.Parsers
{
    public static class StartPositionParser
    {
        public static RoverPosition Parse(string startPositionInfo)
        {
            if (string.IsNullOrWhiteSpace(startPositionInfo))
                throw new ParseException(ApplicationMessages.InvalidRoverStartInfo);

            var parts = startPositionInfo.Split(' ');
            if (parts.Length != 3)
                throw new ParseException(ApplicationMessages.InvalidRoverStartInfo);


            int part1, part2;
            if (int.TryParse(parts[0], out part1) == false)
                throw new ParseException(ApplicationMessages.InvalidRoverStartInfo);
            if (int.TryParse(parts[1], out part2) == false)
                throw new ParseException(ApplicationMessages.InvalidRoverStartInfo);


            var roverStartPosition = RoverPosition.Build(part1, part2, (Orientation)Enum.Parse(typeof(Orientation),parts[2]));
            return roverStartPosition;
        }
    }
}

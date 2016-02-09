using MarsRoversApp.Parsers;
using MarsRoversApp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.Types
{
    public class ExplorerInput
    {
        public PlateauSpecs PlateauSpecs { get; set; }       
        public List<Rover> Rovers { get; set; }

        public ExplorerInput()
        {

        }

        public void Parse(string[] lines)
        {
            ValidateLinesLength(lines);

            Rovers = new List<Rover>();

            PlateauSpecs = PlateauSpecsParser.Parse(lines.First());

            lines = lines.Skip(1).ToArray();

            while (lines.Length > 0)
            {
                var startPos = StartPositionParser.Parse(lines[0]);

                var Movements = RoverMovementParser.Parse(lines[1]);

                var roverInput = new Rover()
                {
                    StartPosition = startPos,
                    Movements = Movements
                };

                Rovers.Add(roverInput);

                lines = lines.Skip(2).ToArray();
            }

        }

        private static void ValidateLinesLength(string[] lines)
        {
            if (lines == null || lines.Length == 0)
                throw new ParseException(ApplicationMessages.EmptyInput);

            if (lines.Length == 1)
                throw new ParseException(ApplicationMessages.SinlgeLineInput);

            if (lines.Length % 2 == 0)
                throw new ParseException(ApplicationMessages.EvenLineInput);
        }
    }
}

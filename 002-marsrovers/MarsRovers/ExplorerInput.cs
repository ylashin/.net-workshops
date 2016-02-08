using MarsRoversApp.Parsers;
using MarsRoversApp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp
{
    
    
    
    public class ExplorerInput
    {
        public PlateauSpecs PlateauSpecs { get; set; }       
        public List<RoverInput> RoverInputs { get; set; }

        public ExplorerInput()
        {

        }

        public void Parse(string[] lines)
        {
            if (lines == null || lines.Length == 0)
                throw new ParseException(ApplicationMessages.EmptyInput);

            RoverInputs = new List<RoverInput>();

            PlateauSpecs = PlateauSpecsParser.Parse(lines.First());
            lines = lines.Skip(1).ToArray();

            while (lines.Length>0)
            {
                var startPos = StartPositionParser.Parse(lines[0]);
                var Movements = RoverMovementParser.Parse(lines[1]);

                var roverInput = new RoverInput()
                {
                    RoverStartPosition = startPos,
                    Movements = Movements
                };

                lines = lines.Skip(2).ToArray();
            }
            
        }
    }
}

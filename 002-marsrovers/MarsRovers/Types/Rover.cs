using MarsRoversApp.DynamicParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.Types
{
    public class Rover
    {
        [Field(false, ' ')]
        public RoverPosition StartPosition { get; set; }
        [Field(false, char.MinValue)]
        public List<MoveAction> Movements { get; set; }

    }
}

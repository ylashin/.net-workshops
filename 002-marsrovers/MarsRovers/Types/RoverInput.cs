using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.Types
{
    public class RoverInput
    {
        public RoverStartPosition RoverStartPosition { get; set; }
        public List<MoveAction> Movements { get; set; }

    }
}

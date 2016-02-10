using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.Types
{
    public class Rover
    {        
        public RoverPosition StartPosition { get; set; }
        public MovementScript MovementScript { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.Types
{
    public class RoverPosition
    {       
        public int X { get; set; }        
        public int Y { get; set; }        
        public Orientation Orientation { get; set; }

        public override bool Equals(object obj)
        {
            var position = obj as RoverPosition;
            if (obj == null)
                return false;

            return (
                position.X == X &&
                position.Y == Y &&
                position.Orientation == Orientation
                );
        }
    }
}

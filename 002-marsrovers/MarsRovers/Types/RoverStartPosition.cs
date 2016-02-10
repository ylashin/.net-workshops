using MarsRoversApp.DynamicParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.Types
{
    public class RoverPosition
    {
        [Column(0)]
        public int X { get; set; }
        [Column(1)]
        public int Y { get; set; }
        [Column(2)]
        public Orientation Orientation { get; set; }

        

        public bool Validate()
        {
            if (X < 1 || Y < 1)
                return false;

            return true;
        }
    }
}

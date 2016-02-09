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

        private RoverPosition(int x,int y,Orientation orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
        }

        public static RoverPosition Build(int width, int height,Orientation orientation)
        {
            if (width < 1 || height < 1)
                throw new ParseException(ApplicationMessages.DimensionsShouldBePositive);

            return RoverPosition.Build(width, height,orientation);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.Types
{
    public class PlateauSpecs
    {
        public int Width { get; }
        public int Height { get; }
        public static PlateauSpecs Build(int width,int height)
        {
            if (width < 1 || height < 1)
                throw new ParseException(ApplicationMessages.DimensionsShouldBePositive);

            return new PlateauSpecs(width, height);
        }
        private PlateauSpecs(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}

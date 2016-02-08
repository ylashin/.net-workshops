using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp
{
    public class ApplicationMessages
    {
        public static readonly string EmptyInput = "Input data does not contain any lines";
        public static readonly string SinlgeLineInput = "Input data contains just a single line";
        public static readonly string EvenLineInput = "Input data has an even number of lines";
        public static readonly string InvalidHeaderNotTwoParts = "Invalid header line - it does not contain to parts of width and height";
        public static readonly string InvalidHeaderNonNumberDimensions = "Invalid header line - dimensions not numberic";
        public static readonly string DimensionsShouldBePositive = "Plateau dimensions should be positive";
        public static readonly string InvalidRoverStartInfo = "Invalid rover start info";
        public static readonly string InvalidRoverStartPosition = "Rover start position outside plateau";
        public static readonly string InvalidRoverMovement = "Invalid rover movement input";

    }
}

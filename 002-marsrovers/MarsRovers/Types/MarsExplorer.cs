using MarsRoversApp.Parsers;
using MarsRoversApp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.Types
{
    public class MarsExplorer
    {
        public Plateau Plateau { get; set; }       
        public IEnumerable<Rover> Rovers { get; set; }

        public MarsExplorer()
        {
        }

        public bool Validate()
        {
            if (Plateau == null || Rovers == null || Rovers.Count() == 0)
                return false;

            if (Plateau.Width <= 0 || Plateau.Height <= 0)
                return false;


            foreach(var rover in Rovers)
            {
                if (rover.StartPosition.X < 0 || rover.StartPosition.Y < 0 )
                    return false;

                if (rover.StartPosition.X > Plateau.Width || rover.StartPosition.Y > Plateau.Height)
                    return false;
            }

            return true;
        }
        
    }
}

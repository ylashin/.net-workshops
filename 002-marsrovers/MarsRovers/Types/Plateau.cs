using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.Types
{
    public class Plateau
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public override bool Equals(object obj)
        {
            var plateau = obj as Plateau;
            if (obj == null)
                return false;

            return (
                plateau.Width == Width &&
                plateau.Height == Height
                );
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.Types
{
    public interface INavigationProvider
    {
        Dictionary<Orientation, Action<Rover, Plateau>> GetMoveActionCommands();
        Dictionary<MovementAction, Action<Rover>> GetRotationCommands();
    }
    public class DefaultNavigationProvider : INavigationProvider
    {
        public Dictionary<Orientation, Action<Rover, Plateau>> GetMoveActionCommands()
        {
            var actionMap = new Dictionary<Orientation, Action<Rover, Plateau>>();

            actionMap.Add(Orientation.N, (r, p) =>
            {
                if (r.Position.Y < p.Height)
                    r.Position.Y++;
            });
            actionMap.Add(Orientation.S, (r, p) => {
                if (r.Position.Y > 0)
                    r.Position.Y--;
            });
            actionMap.Add(Orientation.E, (r, p) => {
                if (r.Position.X < p.Width)
                    r.Position.X++;
            });
            actionMap.Add(Orientation.W, (r, p) => {
                if (r.Position.X > 0)
                    r.Position.X--;
            });
            return actionMap;
        }

        public Dictionary<MovementAction, Action<Rover>> GetRotationCommands()
        {
            var rotationMap = new Dictionary<MovementAction, Action<Rover>>();

            var turnLeftMap = new Dictionary<Orientation, Orientation>()
            {
                { Orientation.N , Orientation.W },
                { Orientation.W , Orientation.S },
                { Orientation.S , Orientation.E },
                { Orientation.E , Orientation.N },
            };

            var turnRightMap = new Dictionary<Orientation, Orientation>()
            {
                { Orientation.N , Orientation.E },
                { Orientation.E , Orientation.S },
                { Orientation.S , Orientation.W },
                { Orientation.W , Orientation.N },
            };

            rotationMap.Add(MovementAction.L, (r) =>
            {
                if (!turnLeftMap.ContainsKey(r.Position.Orientation))
                    throw new ApplicationException("Invalid orientation {r.Position.Orientation} to turn it left");

                r.Position.Orientation = turnLeftMap[r.Position.Orientation];
                
            });

            rotationMap.Add(MovementAction.R, (r) => {

                if (!turnRightMap.ContainsKey(r.Position.Orientation))
                    throw new ApplicationException("Invalid orientation {r.Position.Orientation} to turn it right");

                r.Position.Orientation = turnRightMap[r.Position.Orientation];
                               
            });

            return rotationMap;
        }
    }
}

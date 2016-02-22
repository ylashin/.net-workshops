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

            rotationMap.Add(MovementAction.L, (r) =>
            {
                if (r.Position.Orientation == Orientation.N) // Use a switch or a map
                    r.Position.Orientation = Orientation.W;
                else if (r.Position.Orientation == Orientation.W)
                    r.Position.Orientation = Orientation.S;
                else if (r.Position.Orientation == Orientation.S)
                    r.Position.Orientation = Orientation.E;
                else if (r.Position.Orientation == Orientation.E)
                    r.Position.Orientation = Orientation.N;
            });

            rotationMap.Add(MovementAction.R, (r) => {
                if (r.Position.Orientation == Orientation.N)
                    r.Position.Orientation = Orientation.E;
                else if (r.Position.Orientation == Orientation.E)
                    r.Position.Orientation = Orientation.S;
                else if (r.Position.Orientation == Orientation.S)
                    r.Position.Orientation = Orientation.W;
                else if (r.Position.Orientation == Orientation.W)
                    r.Position.Orientation = Orientation.N;
            });

            return rotationMap;
        }
    }
}

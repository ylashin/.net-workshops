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
        public Plateau Plateau { get;  }       
        // Is this Lazy?
        public IEnumerable<Rover> Rovers { get;  }

        private INavigationProvider NavigationProvider;

        public MarsExplorer(INavigationProvider navigationProvider, Plateau plateau, IEnumerable<Rover> rovers)
        {
            NavigationProvider = navigationProvider;
            Plateau = plateau;
            Rovers = rovers;
        }
        public MarsExplorer(Plateau plateau,IEnumerable<Rover> rovers)
        {
            NavigationProvider = new DefaultNavigationProvider();
            Plateau = plateau;
            Rovers = rovers;
        }

        public bool Validate()
        {
            if (Plateau == null || Rovers == null || !Rovers.Any()) // .Any()
                return false;

            if (Plateau.Width <= 0 || Plateau.Height <= 0)
                return false;


            foreach(var rover in Rovers)
            {
                if (rover.Position.X < 0 || rover.Position.Y < 0 )
                    return false;

                if (rover.Position.X > Plateau.Width || rover.Position.Y > Plateau.Height)
                    return false;
            }

            return true;
        }
        public void ProcessNavigation()
        {            
            // Multiple enumaration? Why have it lazy?
            Rovers.ToList().ForEach(r=>
            {
                r.MovementScript.Movements.ForEach(m => 
                {
                    DoAction(r, m);
                });
            });
        }

        public void DoAction(Rover rover, MovementAction action)
        {
            var moveMap = NavigationProvider.GetMoveActionCommands();
            var rotationMap = NavigationProvider.GetRotationCommands();

            // Alternate: use a map
            switch (action)
            {
                case MovementAction.M:
                    var moveCommand = moveMap[rover.Position.Orientation];
                    moveCommand(rover, Plateau);
                    break;
                case MovementAction.L:
                case MovementAction.R:
                    var rotationCommand = rotationMap[action];
                    rotationCommand(rover);
                    break;
                default:
                    // Is this the correct default action?
                    break;
            }
        }

        

        
    }
}

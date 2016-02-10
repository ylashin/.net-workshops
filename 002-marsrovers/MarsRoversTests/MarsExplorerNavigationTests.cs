using MarsRoversApp.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversTests
{
    [TestFixture]
    public class MarsExplorerNavigationTests
    {
        private MarsExplorer Build(int width, int height, int startX, int startY, Orientation oreintation)
        {
            MarsExplorer x = new MarsExplorer(new Plateau() { Width = width, Height = height },
                new List<Rover>() {
                new Rover()
                {
                     Position = new RoverPosition()
                     {
                          X = startX,
                          Y = startY,
                          Orientation = oreintation
                     }
                }
            });            

            return x;
        }

        [TestCase(10, 10, 2, 2, Orientation.N, MovementAction.M, 2, 3)]
        [TestCase(10, 10, 2, 2, Orientation.S, MovementAction.M, 2, 1)]
        [TestCase(10, 10, 2, 2, Orientation.E, MovementAction.M, 3, 2)]
        [TestCase(10, 10, 2, 2, Orientation.W, MovementAction.M, 1, 2)]
        public void Explorer_RoverActionMove_ForwardsTheRoverOneStep(
            int width,int height,int startX,int startY, Orientation orientation,MovementAction action,
            int expectedx,int expectedy
            )
        {
            MarsExplorer x = Build(width,height,startX,startY,orientation);
            var rover = x.Rovers.ToList()[0];
            x.DoAction(rover,action);

            Assert.AreEqual(rover.Position.X , expectedx);
            Assert.AreEqual(rover.Position.Y, expectedy);
        }


        [TestCase(10, 10, 2, 10, Orientation.N, MovementAction.M, 2, 10)]
        [TestCase(10, 10, 2, 0, Orientation.S, MovementAction.M, 2, 0)]
        [TestCase(10, 10, 10, 2, Orientation.E, MovementAction.M, 10, 2)]
        [TestCase(10, 10, 0, 2, Orientation.W, MovementAction.M, 0, 2)]
        public void Explorer_RoverActionMove_ForwardsTheRoverOneStepKeepWithinPlateau(
            int width, int height, int startX, int startY, Orientation orientation, MovementAction action,
            int expectedx, int expectedy
            )
        {
            MarsExplorer x = Build(width, height, startX, startY, orientation);
            var rover = x.Rovers.ToList()[0];
            x.DoAction(rover, action);

            Assert.AreEqual(rover.Position.X, expectedx);
            Assert.AreEqual(rover.Position.Y, expectedy);
        }


        [TestCase(10, 10, 2, 10, Orientation.N, MovementAction.L, Orientation.W)]
        [TestCase(10, 10, 2, 10, Orientation.W, MovementAction.L, Orientation.S)]
        [TestCase(10, 10, 2, 10, Orientation.S, MovementAction.L, Orientation.E)]
        [TestCase(10, 10, 2, 10, Orientation.E, MovementAction.L, Orientation.N)]
        [TestCase(10, 10, 2, 10, Orientation.N, MovementAction.R, Orientation.E)]
        [TestCase(10, 10, 2, 10, Orientation.E, MovementAction.R, Orientation.S)]
        [TestCase(10, 10, 2, 10, Orientation.S, MovementAction.R, Orientation.W)]
        [TestCase(10, 10, 2, 10, Orientation.W, MovementAction.R, Orientation.N)]

        public void Explorer_RoverActionRotate_ValidNewOrientation(
            int width, int height, int startX, int startY, Orientation orientation, MovementAction action,
            Orientation expected
            )
        {
            MarsExplorer x = Build(width, height, startX, startY, orientation);
            var rover = x.Rovers.ToList()[0];
            x.DoAction(rover, action);

            Assert.AreEqual(rover.Position.Orientation, expected);
        }

    }
}

using MarsRoversApp.Types;
using NUnit.Framework;
using Shouldly;
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

        [TestCase(Orientation.N,  2, 3, TestName = "Starting 2,2 => Move Facing North")] // Simplify by using a setup method
        [TestCase(Orientation.S,  2, 1, TestName = "Starting 2,2 => Move Facing South")]
        [TestCase(Orientation.E,  3, 2, TestName = "Starting 2,2 => Move Facing East")]
        [TestCase(Orientation.W,  1, 2, TestName = "Starting 2,2 => Move Facing West")]
        public void Explorer_RoverActionMove_ForwardsTheRoverOneStep( // Better name
            Orientation orientation,
            int expectedx,int expectedy
            )
        {
            int width = 10;
            int height = 10;
            int startx = 2;
            int starty = 2;
                       
            MarsExplorer x = Build(width,height,startx,starty,orientation);
            var rover = x.Rovers.ToList()[0];
            x.DoAction(rover,MovementAction.M);

            rover.Position.X.ShouldBe(expectedx);
            rover.Position.Y.ShouldBe(expectedy);
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


        [TestCase( Orientation.N, MovementAction.L, Orientation.W, TestName = "Turn left while heading north")]
        [TestCase( Orientation.W, MovementAction.L, Orientation.S, TestName = "Turn left while heading west")]
        [TestCase( Orientation.S, MovementAction.L, Orientation.E, TestName = "Turn left while heading south")]
        [TestCase( Orientation.E, MovementAction.L, Orientation.N, TestName = "Turn left while heading east")]
        [TestCase( Orientation.N, MovementAction.R, Orientation.E, TestName = "Turn right while heading north")]
        [TestCase( Orientation.E, MovementAction.R, Orientation.S, TestName = "Turn right while heading east")]
        [TestCase( Orientation.S, MovementAction.R, Orientation.W, TestName = "Turn right while heading south")]
        [TestCase( Orientation.W, MovementAction.R, Orientation.N, TestName = "Turn right while heading west")]

        public void Explorer_RoverActionRotate_ValidNewOrientation(
            Orientation orientation, MovementAction action,
            Orientation expected
            )
        {
            int width = 10;
            int height = 10;
            int startX = 2;
            int startY = 10;

            MarsExplorer x = Build(width, height, startX, startY, orientation);
            var rover = x.Rovers.ToList()[0];
            x.DoAction(rover, action);

            rover.Position.Orientation.ShouldBe(expected);
        }

    }
}

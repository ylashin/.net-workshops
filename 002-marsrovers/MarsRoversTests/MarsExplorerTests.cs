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
    public class MarsExplorerTests
    {
        [Test]
        public void Explorer_ValidateEmptyContent_ReturnsFalse()
        {
            MarsExplorer x = new MarsExplorer();
            bool result = x.Validate();
            Assert.AreEqual(result, false);
        }

        [Test]
        public void Explorer_ValidateNullRovers_ReturnsFalse()
        {
            MarsExplorer x = new MarsExplorer();
            x.Plateau = new Plateau() { Width = 1, Height = 1 };
            bool result = x.Validate();
            Assert.AreEqual(result, false);
        }

        [Test]
        public void Explorer_ValidateEmptyRovers_ReturnsFalse()
        {
            MarsExplorer x = new MarsExplorer();
            x.Plateau = new Plateau() { Width = 1, Height = 1 };
            x.Rovers = new List<Rover>();
            bool result = x.Validate();
            Assert.AreEqual(result, false);
        }

        [TestCase(0,1)]
        [TestCase(1, 0)]
        [TestCase(0, 0)]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        [TestCase(-1, -1)]
        public void Explorer_ValidatePlateuSpecsNonPositive_ReturnsFalse(int w,int h)
        {
            MarsExplorer x = new MarsExplorer();
            x.Plateau = new Plateau() { Width = h, Height = h };
            bool result = x.Validate();
            Assert.AreEqual(result, false);
        }
        
        
        [TestCase(11, 10)]
        [TestCase(0, 11)]
        [TestCase(11, 11)]
        public void Explorer_ValidateRoverOutsidePlateu_ReturnsFalse(int x, int y)
        {
            MarsExplorer mx = new MarsExplorer();
            mx.Plateau = new Plateau() { Width = 10, Height = 10 };
            mx.Rovers = new List<Rover>() {
                new Rover()
                {
                     StartPosition = new RoverPosition()
                     {
                          X = x,
                          Y = y
                     }
                }
            };
            
            bool result = mx.Validate();
            Assert.AreEqual(result, false);
        }

        
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        [TestCase(-1, -1)]
        public void Explorer_ValidateRoverPositionNegative_ReturnsFalse(int x, int y)
        {
            MarsExplorer mx = new MarsExplorer();
            mx.Plateau = new Plateau() { Width = 10, Height = 10 };
            mx.Rovers = new List<Rover>() {
                new Rover()
                {
                     StartPosition = new RoverPosition()
                     {
                          X = x,
                          Y = y
                     }
                }
            };

            bool result = mx.Validate();
            Assert.AreEqual(result, false);
        }

    }
}

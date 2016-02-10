using MarsRoversApp.Parsers;
using MarsRoversApp.Types;
using NUnit.Framework;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversTests
{
    [TestFixture]
    public class MarsGrammerTests
    {

        [Test]
        public void Grammar_WhenParsingDigits_ReturnsParsedNumber()
        {
            var input = "1234";
            var number = MarsGrammar.Number.Parse(input);
            Assert.AreEqual(int.Parse(number), 1234);
        }

        [Test]
        public void Grammar_WhenParsingDigitsWithSpaces_ParsesAsMuchAsItCan()
        {
            var input = "1234 abc";
            var number = MarsGrammar.Number.Parse(input);
            Assert.AreEqual(int.Parse(number), 1234);
        }


        [Test]
        public void Grammar_WhenParsingTextStaringWithNonDigit_ThrowsException()
        {
            var input = "a1234";

            Assert.Throws<ParseException>(() => MarsGrammar.Number.Parse(input));
        }


        [Test]
        public void Plateau_WhenFedWithWidthAndHeight_ShouldParseThem()
        {
            var input = "5 7";
            var plateau = MarsGrammar.Plateau.Parse(input);
            Assert.AreEqual(5, plateau.Width);
            Assert.AreEqual(7, plateau.Height);
        }

        [TestCase("a 7")]
        [TestCase("7 a")]
        [TestCase("a b")]
        public void Plateau_WhenFedWithRubbish_ShouldThrowException(string input)
        { 
            Assert.Throws<ParseException>(() => MarsGrammar.Plateau.Parse(input));
        }

        [TestCase("5 7 N")]
        [TestCase("5 7 S")]
        [TestCase("5 7 E")]
        [TestCase("5 7 W")]
        public void RoverPosition_WhenFedWithValidInput_ShouldParse(string input)
        {            
            var position = MarsGrammar.RoverPosition.Parse(input);
            Assert.AreEqual(5, position.X);
            Assert.AreEqual(7, position.Y);
            Assert.AreEqual(
                (Orientation)Enum.Parse(typeof(Orientation), input.Split(' ')[2]),
                position.Orientation);
        }

        
        [TestCase("a b N")]
        [TestCase("a b c")]
        [TestCase("5 b N")]
        [TestCase("5 7")]
        [TestCase("5")]
        public void RoverPosition_WhenFedWithRubbish_ShouldThrowException(string input)
        {
            Assert.Throws<ParseException>(() => MarsGrammar.RoverPosition.Parse(input));
        }

        [TestCase("5 7 L")]
        public void RoverPosition_WhenFedWithRubbishOrientation_ShouldThrowArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => MarsGrammar.RoverPosition.Parse(input));
        }

        //[TestCase("LR1")]
        //public void MovementScript_WhenFedWithRubbishInput_ShouldThrowArgumentException(string input)
        //{
        //    var x = MarsGrammar.MovementScript.Parse(input);
        //    //Assert.Throws<ArgumentException>(() => MarsGrammar.MovementScript.Parse(input));
        //}

        [Test]
        public void MovementScript_WhenFedWithValidInput_ShouldParseThem()
        {
            var input = "LRMMRL";
            var script = MarsGrammar.MovementScript.Parse(input);
            Assert.AreEqual(script.Movements[0], MovementAction.L);
            Assert.AreEqual(script.Movements[1], MovementAction.R);
            Assert.AreEqual(script.Movements[2], MovementAction.M);
            Assert.AreEqual(script.Movements[3], MovementAction.M);
            Assert.AreEqual(script.Movements[4], MovementAction.R);
            Assert.AreEqual(script.Movements[5], MovementAction.L);
        }

        [Test]
        public void Rover_WhenFedWithValidInput_ShouldParseThem()
        {
            var input = "5 7 N\r\nLRMMRL";
            var rover = MarsGrammar.Rover.Parse(input);
            var script = rover.MovementScript;
            var position = rover.Position;

            Assert.AreEqual(position , new RoverPosition()
            { X = 5 , Y = 7 , Orientation = Orientation.N });

            Assert.AreEqual(script.Movements.Count,6);
            Assert.AreEqual(script.Movements[0], MovementAction.L);
            Assert.AreEqual(script.Movements[1], MovementAction.R);
            Assert.AreEqual(script.Movements[2], MovementAction.M);
            Assert.AreEqual(script.Movements[3], MovementAction.M);
            Assert.AreEqual(script.Movements[4], MovementAction.R);
            Assert.AreEqual(script.Movements[5], MovementAction.L);
        }


        [Test]
        public void Rovers_WhenFedWithValidInput_ShouldParseThem()
        {
            var input = "5 7 N\r\nLRMMRL\r\n1 3 S\r\nMRL";
            var rovers = MarsGrammar.Rovers.Parse(input).ToList();

            Assert.AreEqual(rovers.Count,2);

            var script = rovers[0].MovementScript;
            var position = rovers[0].Position;

            Assert.AreEqual(position, new RoverPosition()
            { X = 5, Y = 7, Orientation = Orientation.N });

            Assert.AreEqual(script.Movements.Count, 6);
            Assert.AreEqual(script.Movements[0], MovementAction.L);
            Assert.AreEqual(script.Movements[1], MovementAction.R);
            Assert.AreEqual(script.Movements[2], MovementAction.M);
            Assert.AreEqual(script.Movements[3], MovementAction.M);
            Assert.AreEqual(script.Movements[4], MovementAction.R);
            Assert.AreEqual(script.Movements[5], MovementAction.L);

            script = rovers[1].MovementScript;
            position = rovers[1].Position;

            Assert.AreEqual(position, new RoverPosition()
            { X = 1, Y = 3, Orientation = Orientation.S });

            Assert.AreEqual(script.Movements.Count, 3);
            Assert.AreEqual(script.Movements[0], MovementAction.M);
            Assert.AreEqual(script.Movements[1], MovementAction.R);
            Assert.AreEqual(script.Movements[2], MovementAction.L);
            
        }

        [Test]
        public void MarsExplorer_WhenFedWithValidInput_ShouldParse()
        {
            var input = @"5 5
                        1 2 N
                        LMLMLMLMM
                        3 3 E
                        MMRMMRMRRM";
            var explorer = MarsGrammar.MarsExplorer.Parse(input);
            Assert.AreEqual(explorer.Rovers.Count(),2);
            Assert.AreEqual(explorer.Plateau, new Plateau()
            { Width = 5, Height = 5 });
            
        }

        [Test]
        public void MarsExplorer_WhenFedWithInValidMovementInput_ShouldThrowException()
        {
            var input = @"5 5
                        1 2 N
                        LMLMLMLMM1
                        3 3 E
                        MMRMMRMRRM";
                        
            Assert.Throws<ParseException>(() => MarsGrammar.MarsExplorer.Parse(input));

        }

        [Test]
        public void MarsExplorer_WhenFedWithInValidEndOfFile_ShouldThrowException()
        {
            var input = @"5 5
                        1 2 N
                        LMLMLMLMM
                        3 3 E
                        MMRMMRMRRM1";

            Assert.Throws<ParseException>(() => MarsGrammar.MarsExplorer.Parse(input));

        }

        [Test]
        public void MarsExplorer_WhenFedWithInValidPlateu_ShouldThrowException()
        {
            var input = @"5 5 1
                        1 2 N
                        LMLMLMLMM
                        3 3 E
                        MMRMMRMRRM";

            Assert.Throws<ParseException>(() => MarsGrammar.MarsExplorer.Parse(input));

        }


        [Test]
        public void MarsExplorer_MissingMovements_ShouldThrowException()
        {
            var input = @"5 5 1
                        1 2 N
                        LMLMLMLMM
                        3 3 E
                               ";

            Assert.Throws<ParseException>(() => MarsGrammar.MarsExplorer.Parse(input));

        }

    }
}

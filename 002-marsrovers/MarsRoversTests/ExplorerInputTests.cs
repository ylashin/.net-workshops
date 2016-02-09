using MarsRoversApp;
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
    public class ExplorerInputTests
    {      
        [Test]
        public void ExplorerInput_WhenParsingNoLines_ShouldThrowException()
        {
            ExplorerInput explorerInput = new ExplorerInput();
            var ex = Assert.Catch<ParseException>(() => explorerInput.Parse(new string[] { }));
            StringAssert.Contains(ApplicationMessages.EmptyInput, ex.Message);
        }

        [Test]
        public void ExplorerInput_WhenParsingSingleLine_ShouldThrowException()
        {
            ExplorerInput explorerInput = new ExplorerInput();

            var ex = Assert.Catch<ParseException>(() => explorerInput.Parse(new string[] {"5 5" }));
            StringAssert.Contains(ApplicationMessages.SinlgeLineInput, ex.Message);
        }

        [Test]
        public void ExplorerInput_WhenParsingEvenNumberOfLines_ShouldThrowException()
        {
            ExplorerInput explorerInput = new ExplorerInput();

            var ex = Assert.Catch<ParseException>(() => explorerInput.Parse(new string[] { "5 5" , "1 2 N", "LMLMLMLMM", "3 3 E" }));
            StringAssert.Contains(ApplicationMessages.EvenLineInput, ex.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("       ")]
        [TestCase("55")]
        [TestCase("5 4 5")]
        [TestCase("5,3")]
        [TestCase(" 5 3")]
        [TestCase("5 3 ")]
        [TestCase("5  3")]
        public void ExplorerInput_WhenParsingInvalidHeaderNotTwoParts_ShouldThrowException(string header)
        {
            ExplorerInput explorerInput = new ExplorerInput();

            var ex = Assert.Catch<ParseException>(() 
                => explorerInput.Parse(new string[] { header, "1 2 N", "LMLMLMLMM" }));
            StringAssert.Contains(ApplicationMessages.InvalidHeaderNotTwoParts, ex.Message);
        }


       
        [TestCase("a 5")]
        [TestCase("5 a")]
        [TestCase("a b")]        
        public void ExplorerInput_WhenParsingInvalidHeaderNonNumberDimensions_ShouldThrowException(string header)
        {
            ExplorerInput explorerInput = new ExplorerInput();

            var ex = Assert.Catch<ParseException>(()
                => explorerInput.Parse(new string[] { header, "1 2 N", "LMLMLMLMM" }));
            StringAssert.Contains(ApplicationMessages.InvalidHeaderNonNumberDimensions, ex.Message);
        }


        [TestCase("-1 2")]
        [TestCase("-2 3")]
        [TestCase("-2 -3")]
        [TestCase("1 0")]
        [TestCase("0 1")]
        [TestCase("0 0")]
        public void ExplorerInput_WhenParsingNegativeDimenstionsInHeader_ShouldThrowException(string header)
        {
            ExplorerInput explorerInput = new ExplorerInput();

            var ex = Assert.Catch<ParseException>(()
                => explorerInput.Parse(new string[] { header, "1 2 N", "LMLMLMLMM" }));
            StringAssert.Contains(ApplicationMessages.DimensionsShouldBePositive, ex.Message);
        }


        [TestCase("5 3")]
        public void ExplorerInput_WhenParsingValidHeader_ShouldAssignItToWidthAndHeight(string header)
        {
            ExplorerInput explorerInput = new ExplorerInput();

            explorerInput.Parse(new string[] { header, "1 2 N", "LMLMLMLMM" });

            int width = int.Parse(header.Split(' ')[0]);
            int height = int.Parse(header.Split(' ')[1]);

            Assert.AreEqual(explorerInput.PlateauSpecs.Width, width);
            Assert.AreEqual(explorerInput.PlateauSpecs.Height, height);
        }

        

        [TestCase("")] // empty
        [TestCase(null)] // null
        [TestCase("      ")]    // white space
        [TestCase("1 2")]   // 2 parts
        [TestCase("1 2 N 4")] // more than 3 parts
        [TestCase("1 2 A")]   // direction is not N S E W
        [TestCase("a 2 N")]   // non numeric position
        [TestCase("2 a N")]   // non numeric position
        [TestCase("a b N")]   // non numeric position
        [TestCase("-1 1 N")]   // negative position
        [TestCase("1 -1 N")]   // negative position
        [TestCase("-1 -1 N")]   // negative position
        [TestCase(" 1 2 N")]   // starts with a space
        [TestCase("1 2 N ")]   // ends with a space
        [TestCase("1 2  N")]   // contains more than one space in middle 
        public void ExplorerInput_WhenParsingInvalidRoverStartInfo_ShouldThrowException(string roverStartInfo)
        {
            ExplorerInput explorerInput = new ExplorerInput();

            var ex = Assert.Catch<ParseException>(()
                => explorerInput.Parse(new string[] { "5 7", roverStartInfo, "LMLMLMLMM" }));
            StringAssert.Contains(ApplicationMessages.InvalidRoverStartInfo, ex.Message);
        }


       
        [TestCase("10 1 N","5","7")]   
        [TestCase("1 10 N", "5", "7")]   
        [TestCase("10 10 N", "5", "7")]    
        public void ExplorerInput_WhenRoverOutsidePlateau_ShouldThrowException(string roverStartInfo,string width,string height)
        {
            ExplorerInput explorerInput = new ExplorerInput();

            var ex = Assert.Catch<ParseException>(()
                => explorerInput.Parse(new string[] { width + " " + height, roverStartInfo, "LMLMLMLMM" }));
            StringAssert.Contains(ApplicationMessages.InvalidRoverStartPosition, ex.Message);
        }



        [TestCase("1 2 N", "5", "7",1,2,"N")]
        [TestCase("3 4 S", "5", "7",3,4,"S")]
        [TestCase("5 6 E", "5", "7",5,6,"E")]
        [TestCase("7 8 W", "9", "9",7,8,"W")]
        public void ExplorerInput_WhenParsingValidRoverStartPos_Should(string roverStartInfo,
            string width, string height,
            int expectedX,int expectedY,string expectedOrientation)
        {
            ExplorerInput explorerInput = new ExplorerInput();

            explorerInput.Parse(new string[] { width + " " + height, roverStartInfo, "LMLMLMLMM" });

            Assert.AreEqual(explorerInput.Rovers[0].StartPosition.X, expectedX);
            Assert.AreEqual(explorerInput.Rovers[0].StartPosition.Y, expectedY);
            Assert.AreEqual(explorerInput.Rovers[0].StartPosition.Orientation.ToString(), expectedOrientation);
        }


        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        [TestCase("LMA")]
        [TestCase(" LM")]
        [TestCase("LM ")]
        [TestCase("L M")]
        public void ExplorerInput_WhenParsingInvalidRoverMovement_ShouldThrowException(string movementLine)
        {
            ExplorerInput explorerInput = new ExplorerInput();
            
            var ex = Assert.Catch<ParseException>(()
                => explorerInput.Parse(new string[] { "5 5", "1 2 N", movementLine })
            );
            StringAssert.Contains(ApplicationMessages.InvalidRoverMovement, ex.Message);

            
        }


    }
}

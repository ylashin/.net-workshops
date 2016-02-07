using NumberWorderApp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberWorderTests
{ 

    [TestFixture]
    public class NumberWorderTests
    {        
        [TestCase("")]
        [TestCase("     ")]
        [TestCase(null)]
        public void NumberWorder_WhenParsingNullOrEmptyText_ShouldThrowArgumentException(string input)
        {
            NumberWorder numberWorder = new NumberWorder();
            var ex = Assert.Catch<ArgumentException>(() => numberWorder.Parse(input));
            StringAssert.Contains(NumberWorder.EmptyInputMessage, ex.Message);
        }


        [TestCase("1234567890", "ONETWOTHREEFOURFIVESIXSEVENEIGHTNINEZERO")]
        [TestCase("32105", "THREETWOONEZEROFIVE")]
        [TestCase("7", "SEVEN")]
        public void NumberWorder_WhenParsingValidNumericInput_ShouldReturnParsedWording(string input,string expected)
        {
            NumberWorder numberWorder = new NumberWorder();
            var outcome = numberWorder.Parse(input);
            Assert.AreEqual(outcome, expected);
        }

        [TestCase("a13")]
        [TestCase("1%3")]
        [TestCase("13_")]
        [TestCase("12 3")]
        [TestCase(" 123")]
        [TestCase("123 ")]
        [TestCase("-123")]
        public void NumberWorder_WhenParsingTextWithAlphaCharacters_ShouldThrowArgumentException(string input)
        {
            NumberWorder numberWorder = new NumberWorder();            
            var ex = Assert.Catch<ArgumentException>(() => numberWorder.Parse(input));
            StringAssert.Contains(NumberWorder.NonNumericInputMessage, ex.Message);
        }
    }
}



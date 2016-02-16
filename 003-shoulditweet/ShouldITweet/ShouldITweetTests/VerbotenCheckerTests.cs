using NUnit.Framework;
using ShouldITweet.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShouldITweetTests
{
    [TestFixture]
    public class VerbotenCheckerTests
    {
        [TestCase("")]
        [TestCase("        ")]
        [TestCase(null)]
        public void VerbotenChecker_WhenCheckingEmptyText_ShouldReturnTrue(string text)
        {
            VerbotenChecker checker = new VerbotenChecker();
            var result = checker.ValidateText(text);
            Assert.AreEqual(result, true);
        }
    }
}

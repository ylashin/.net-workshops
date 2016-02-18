using NSubstitute;
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
            IVerbotenPhraseProvider provider = Substitute.For<IVerbotenPhraseProvider>();
            VerbotenChecker checker = new VerbotenChecker(provider);

            var result = checker.ValidateText(text);

            Assert.AreEqual(result.IsSafeText, true);
        }

        [Test]        
        public void VerbotenChecker_WhenCheckingTextWithVerbotenPhrases_ShouldReturnFalsePlusViolations()
        {
            var text = "This is a tweet containing verboten";
            IVerbotenPhraseProvider provider = Substitute.For<IVerbotenPhraseProvider>();
            provider.GetVerbotenPhrases().ReturnsForAnyArgs(new List<string> { "verboten" });
            VerbotenChecker checker = new VerbotenChecker(provider);

            var result = checker.ValidateText(text);

            Assert.AreEqual(result.IsSafeText, false);
            Assert.AreEqual(result.Violations.Count, 1);
            Assert.AreEqual(result.Violations[0], "verboten");
        }

        [Test]
        public void VerbotenChecker_WhenCheckingTextWithVerbotenPhraseWithDifferentCase_ShouldReturnFalsePlusViolations()
        {
            var text = "This is a tweet containing verboten";
            IVerbotenPhraseProvider provider = Substitute.For<IVerbotenPhraseProvider>();
            provider.GetVerbotenPhrases().ReturnsForAnyArgs(new List<string> { "Verboten" });
            VerbotenChecker checker = new VerbotenChecker(provider);

            var result = checker.ValidateText(text);

            Assert.AreEqual(result.IsSafeText, false);
            Assert.AreEqual(result.Violations.Count, 1);
            Assert.AreEqual(result.Violations[0], "Verboten");
        }

        [Test]
        public void VerbotenChecker_WhenCheckingTextWithMultipleVerbotenPhrases_ShouldReturnFalsePlusViolations()
        {
            var text = "This is a tweet containing verboten1 and verboten2";
            IVerbotenPhraseProvider provider = Substitute.For<IVerbotenPhraseProvider>();
            provider.GetVerbotenPhrases().ReturnsForAnyArgs(new List<string> { "Verboten2", "Verboten1" });
            VerbotenChecker checker = new VerbotenChecker(provider);

            var result = checker.ValidateText(text);

            Assert.AreEqual(result.IsSafeText, false);
            Assert.AreEqual(result.Violations.Count, 2);
            Assert.IsTrue(result.Violations.Contains("Verboten1"));
            Assert.IsTrue(result.Violations.Contains("Verboten2"));
        }

        [Test]
        public void VerbotenChecker_WhenCheckingTextWithoutVerbotenPhrases_ShouldReturnTrue()
        {
            var text = "This is a tweet containing good safe text";
            IVerbotenPhraseProvider provider = Substitute.For<IVerbotenPhraseProvider>();
            provider.GetVerbotenPhrases().ReturnsForAnyArgs(new List<string> { "Verboten2", "Verboten1" });
            VerbotenChecker checker = new VerbotenChecker(provider);

            var result = checker.ValidateText(text);

            Assert.AreEqual(result.IsSafeText, true);
        }


        [Test]
        public void VerbotenChecker_WhenCheckingTextWithEmptyVerbotenPhraseProvider_ShouldReturnTrue()
        {
            var text = "This is a tweet containing verboten1 and verboten2";
            IVerbotenPhraseProvider provider = Substitute.For<IVerbotenPhraseProvider>();
            provider.GetVerbotenPhrases().ReturnsForAnyArgs(new List<string> {  });
            VerbotenChecker checker = new VerbotenChecker(provider);

            var result = checker.ValidateText(text);

            Assert.AreEqual(result.IsSafeText, true);
        }

        [Test]
        public void VerbotenChecker_WhenCheckingTextWithVerbotenPhrasesOfNullOrWhiteSpace_ShouldReturnTrue()
        {
            var text = "This is a tweet containing verboten1 and verboten2";
            IVerbotenPhraseProvider provider = Substitute.For<IVerbotenPhraseProvider>();
            provider.GetVerbotenPhrases().ReturnsForAnyArgs(new List<string> { null,"","   " });
            VerbotenChecker checker = new VerbotenChecker(provider);

            var result = checker.ValidateText(text);

            Assert.AreEqual(result.IsSafeText, true);
        }

        [Test]
        public void VerbotenChecker_WhenCheckingTextWhileVerbotenProviderIsNull_ShouldThrowException()
        {
            var text = "This is a tweet containing good safe text";
            
            VerbotenChecker checker = new VerbotenChecker(null);

            var ex = Assert.Catch<ArgumentException>(() => checker.ValidateText(text));
            StringAssert.Contains(VerbotenChecker.NullVerbotenPhraseProvider, ex.Message);            
        }

        
    }
}

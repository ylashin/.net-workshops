using NSubstitute;
using NUnit.Framework;
using ShouldITweet2.Logic;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShouldITweet2Tests
{
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

            result.IsSafeText.ShouldBe(true);
        }

        [Test]
        public void VerbotenChecker_WhenCheckingTextWithVerbotenPhrases_ShouldReturnFalsePlusViolations()
        {
            var text = "This is a tweet containing verboten";
            IVerbotenPhraseProvider provider = Substitute.For<IVerbotenPhraseProvider>();
            provider.GetVerbotenPhrases().ReturnsForAnyArgs(new List<string> { "verboten" });
            VerbotenChecker checker = new VerbotenChecker(provider);

            var result = checker.ValidateText(text);

            result.IsSafeText.ShouldBe(false);
            result.Violations.Count.ShouldBe(1);
            result.Violations[0].ShouldBe("verboten");
        }

        [Test]
        public void VerbotenChecker_WhenCheckingTextWithVerbotenPhraseWithDifferentCase_ShouldReturnFalsePlusViolations()
        {
            var text = "This is a tweet containing verboten";
            IVerbotenPhraseProvider provider = Substitute.For<IVerbotenPhraseProvider>();
            provider.GetVerbotenPhrases().ReturnsForAnyArgs(new List<string> { "Verboten" });
            VerbotenChecker checker = new VerbotenChecker(provider);

            var result = checker.ValidateText(text);

            result.IsSafeText.ShouldBe(false);
            result.Violations.Count.ShouldBe(1);
            result.Violations[0].ShouldBe("Verboten");
        }

        [Test]
        public void VerbotenChecker_WhenCheckingTextWithMultipleVerbotenPhrases_ShouldReturnFalsePlusViolations()
        {
            var text = "This is a tweet containing verboten1 and verboten2";
            IVerbotenPhraseProvider provider = Substitute.For<IVerbotenPhraseProvider>();
            provider.GetVerbotenPhrases().ReturnsForAnyArgs(new List<string> { "Verboten2", "Verboten1" });
            VerbotenChecker checker = new VerbotenChecker(provider);

            var result = checker.ValidateText(text);

            result.IsSafeText.ShouldBe(false);
            result.Violations.Count.ShouldBe(2);
            result.Violations.Contains("Verboten1").ShouldBeTrue();
            result.Violations.Contains("Verboten2").ShouldBeTrue();
        }

        [Test]
        public void VerbotenChecker_WhenCheckingTextWithoutVerbotenPhrases_ShouldReturnTrue()
        {
            var text = "This is a tweet containing good safe text";
            IVerbotenPhraseProvider provider = Substitute.For<IVerbotenPhraseProvider>();
            provider.GetVerbotenPhrases().ReturnsForAnyArgs(new List<string> { "Verboten2", "Verboten1" });
            VerbotenChecker checker = new VerbotenChecker(provider);

            var result = checker.ValidateText(text);

            result.IsSafeText.ShouldBe(true);
        }


        [Test]
        public void VerbotenChecker_WhenCheckingTextWithEmptyVerbotenPhraseProvider_ShouldReturnTrue()
        {
            var text = "This is a tweet containing verboten1 and verboten2";
            IVerbotenPhraseProvider provider = Substitute.For<IVerbotenPhraseProvider>();
            provider.GetVerbotenPhrases().ReturnsForAnyArgs(new List<string> { });
            VerbotenChecker checker = new VerbotenChecker(provider);

            var result = checker.ValidateText(text);

            result.IsSafeText.ShouldBe(true);
        }

        [Test]
        public void VerbotenChecker_WhenCheckingTextWithVerbotenPhrasesOfNullOrWhiteSpace_ShouldReturnTrue()
        {
            var text = "This is a tweet containing verboten1 and verboten2";
            IVerbotenPhraseProvider provider = Substitute.For<IVerbotenPhraseProvider>();
            provider.GetVerbotenPhrases().ReturnsForAnyArgs(new List<string> { null, "", "   " });
            VerbotenChecker checker = new VerbotenChecker(provider);

            var result = checker.ValidateText(text);

            result.IsSafeText.ShouldBe(true);
        }

        [Test]
        public void VerbotenChecker_WhenCheckingTextWhileVerbotenProviderIsNull_ShouldThrowException()
        {            
            var ex = Assert.Catch<ArgumentException>(() => new VerbotenChecker(null));
            StringAssert.Contains(VerbotenChecker.NullVerbotenPhraseProvider, ex.Message);
        }


    }
}

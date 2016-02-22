using NSubstitute;
using NUnit.Framework;
using ShouldITweetClient.Controllers;
using ShouldITweetClient.Data;
using ShouldITweetClient.Logic;
using ShouldITweetClient.Models;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShouldITweetTests
{
    [TestFixture]
    public class AppControllerTests
    {
        [Test]
        public void AppController_ValidateEmptyTweet_ShouldThrowBadRequest()
        {
            var tweet = new Tweet();
            var checker = Substitute.For<IVerbotenChecker>();
            var controller = BuildController(checker);

            var ex = Assert.Catch<HttpResponseException>(() => controller.Post(tweet));
            ex.Response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        [Test]
        public void AppController_ValidateTweetWithGoodText_ShouldHaveResponseWithTruePassed()
        {
            var tweet = new Tweet() { Text = "test" };
            var checker = Substitute.For<IVerbotenChecker>();
            checker.ValidateText(Arg.Any<string>())
                .ReturnsForAnyArgs(VerbotenCheckerResponse.GetHappyEmptyResponse());
            var controller = BuildController(checker);

            var response = controller.Post(tweet);
            response.VerbotenCheckPassed.ShouldBe(true);
        }
        [Test]
        public void AppController_ValidateTweetWithBadText_ShouldHaveResponseWithFalsePassed()
        {
            var tweet = new Tweet() { Text = "test" };
            var checker = Substitute.For<IVerbotenChecker>();
            var failedResponse = VerbotenCheckerResponse.GetHappyEmptyResponse();
            failedResponse.FailItAndAddViolation("violation");
            checker.ValidateText(Arg.Any<string>())
                .ReturnsForAnyArgs(failedResponse);
            var controller = BuildController(checker);

            var response = controller.Post(tweet);
            response.VerbotenCheckPassed.ShouldBe(false);
            response.Violations[0].ShouldBe("violation");
        }

        private AppController BuildController(IVerbotenChecker checker)
        {
            var repo = Substitute.For<IRepository<VerbotenPhrase>>();
            return new AppController(checker, repo);
        }
    }
}

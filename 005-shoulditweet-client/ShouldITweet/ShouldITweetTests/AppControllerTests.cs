using NSubstitute;
using NUnit.Framework;
using ShouldITweetClient.Controllers;
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
            var controller = new AppController(checker);

            var ex = Assert.Catch<HttpResponseException>(() => controller.Post(tweet));
            ex.Response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}

using ShouldITweet2.Logic;
using ShouldITweet2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShouldITweet2.Controllers
{
    public class HomeController : Controller
    {
        public IVerbotenChecker VerbotenChecker { get; set; }
        public HomeController(IVerbotenChecker verbotenChecker)
        {
            VerbotenChecker = verbotenChecker;

        }
        public ActionResult Index()
        {
            return View(new Tweet() { Text = "" });
        }

        [HttpPost]
        public ActionResult Index(Tweet tweet)
        {
            if (!ModelState.IsValid)
            {   
                return View(tweet);
            }

            var checkResponse = VerbotenChecker.ValidateText(tweet.Text);
            tweet.VerbotenCheckPassed = checkResponse.IsSafeText;
            tweet.Violations = checkResponse.Violations;

            return View(tweet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
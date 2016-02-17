using ShouldITweet.Logic;
using ShouldITweet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShouldITweet.Controllers
{
    public class HomeController : Controller
    {
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

            var provider = new FixedVerbotenPhraseProvider();
            var checker = new VerbotenChecker(provider);

            tweet.VerbotenCheckPassed = checker.ValidateText(tweet.Text);

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
using Serilog;
using ShouldITweetClient.Logic;
using ShouldITweetClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShouldITweetClient.Controllers
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
    }
}
using ShouldITweetClient.Logic;
using ShouldITweetClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShouldITweetClient.Controllers
{
    public class AppController : ApiController
    {
        public IVerbotenChecker VerbotenChecker { get; set; }
        public AppController(IVerbotenChecker verbotenChecker)
        {
            VerbotenChecker = verbotenChecker;

        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public Tweet Post([FromBody]Tweet tweet)
        {
            if (tweet==null || string.IsNullOrWhiteSpace(tweet.Text))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var checkResponse = VerbotenChecker.ValidateText(tweet.Text);

            tweet.VerbotenCheckPassed = checkResponse.IsSafeText;
            tweet.Violations = checkResponse.Violations;

            return tweet;

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
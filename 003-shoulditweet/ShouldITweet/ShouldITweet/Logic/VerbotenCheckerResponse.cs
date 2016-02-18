using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShouldITweet.Logic
{
    public class VerbotenCheckerResponse
    {
        public bool IsSafeText { get; set; }
        public IList<string> Violations { get; set; }
    }
}
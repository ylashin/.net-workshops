using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShouldITweet.Logic
{
    public interface IVerbotenChecker
    {
        bool ValidateText(string text);
    }
    public class VerbotenChecker : IVerbotenChecker
    {
        public bool ValidateText(string text)
        {
            throw new NotImplementedException();
        }
    }
}
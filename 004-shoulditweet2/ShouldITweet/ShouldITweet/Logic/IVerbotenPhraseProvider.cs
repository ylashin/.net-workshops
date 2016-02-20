using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShouldITweet2.Logic
{
    public interface IVerbotenPhraseProvider
    {
        IEnumerable<string> GetVerbotenPhrases();
    }
}
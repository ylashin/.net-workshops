using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShouldITweetClient.Logic
{
    public interface IVerbotenPhraseProvider
    {
        IEnumerable<string> GetVerbotenPhrases();
    }
}
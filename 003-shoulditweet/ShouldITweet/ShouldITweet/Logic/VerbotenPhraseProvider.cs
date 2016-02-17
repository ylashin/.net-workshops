using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShouldITweet.Logic
{
    public interface IVerbotenPhraseProvider
    {
        IEnumerable<string> GetVerbotenPhrases();
    }
    public class FixedVerbotenPhraseProvider : IVerbotenPhraseProvider
    {
        public IEnumerable<string> GetVerbotenPhrases()
        {
            return new List<string>()
            {
                "Trump",
                "#selfie",
                "hillary"
            }.AsEnumerable();
        }
    }
}
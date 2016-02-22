using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShouldITweetClient.Logic
{
    
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
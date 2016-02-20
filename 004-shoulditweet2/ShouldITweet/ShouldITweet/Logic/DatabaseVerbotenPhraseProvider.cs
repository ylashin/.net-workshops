using ShouldITweet2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShouldITweet2.Logic
{
   
    public class DatabaseVerbotenPhraseProvider : IVerbotenPhraseProvider
    {
        public IEnumerable<string> GetVerbotenPhrases()
        {
            using (var db = new ShouldITweetDbContext())
            {
                return db.VerbotenPhrases.Select(v => v.Phrase).ToList();
            }
        }
    }
}
using ShouldITweet2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShouldITweet2.Logic
{
   
    public class DatabaseVerbotenPhraseProvider : IVerbotenPhraseProvider
    {
        private IRepository Repository;

        public DatabaseVerbotenPhraseProvider(IRepository repository)
        {
            Repository = repository;
        }

        public IEnumerable<string> GetVerbotenPhrases()
        {
            return Repository.GetAll<VerbotenPhrase>().Select(v => v.Phrase).ToList();
        }
    }
}
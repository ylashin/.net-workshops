﻿using ShouldITweet2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShouldITweet2.Logic
{
   
    public class DatabaseVerbotenPhraseProvider : IVerbotenPhraseProvider
    {
        private IRepository<VerbotenPhrase> Repository;

        public DatabaseVerbotenPhraseProvider(IRepository<VerbotenPhrase> repository)
        {
            Repository = repository;
        }

        public IEnumerable<string> GetVerbotenPhrases()
        {
            return Repository.GetAll().Select(v => v.Phrase).ToList();
        }
    }
}
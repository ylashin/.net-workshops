using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShouldITweet2.Data
{
    public class ShouldITweetDbContext : DbContext
    {
        public ShouldITweetDbContext() : base("ShouldITweet2Database")
        {

        }
        public DbSet<VerbotenPhrase> VerbotenPhrases { get; set; }
    }
}
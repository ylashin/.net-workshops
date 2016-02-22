using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShouldITweetClient.Data
{
    public class ShouldITweetDbContext : DbContext
    {
        public ShouldITweetDbContext() : base("ShouldITweetClientDatabase")
        {

        }
        public DbSet<VerbotenPhrase> VerbotenPhrases { get; set; }
    }
}
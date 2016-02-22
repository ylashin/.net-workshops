namespace ShouldITweet2.Migrations
{
    using Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ShouldITweet2.Data.ShouldITweetDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }      

        protected override void Seed(ShouldITweetDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            
            if (!context.VerbotenPhrases.Any(a=>a.Phrase== "#selfie"))
                context.VerbotenPhrases.Add(VerbotenPhrase.Create("#selfie"));

            if (!context.VerbotenPhrases.Any(a => a.Phrase == "Trump"))
                context.VerbotenPhrases.Add(VerbotenPhrase.Create("Trump"));
        }
    }
}

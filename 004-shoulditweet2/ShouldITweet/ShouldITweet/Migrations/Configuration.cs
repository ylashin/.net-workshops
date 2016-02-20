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
            
            context.VerbotenPhrases.AddOrUpdate(
                p => p.Id,
                new VerbotenPhrase (Guid.Parse("{51FBEC78-2F16-4BA9-9815-EDF511F17668}"), "#selfie", DateTime.UtcNow),
                new VerbotenPhrase(Guid.Parse("{0EC1684B-80C2-4ABA-A6B9-2EA2C2997590}"), "Trump", DateTime.UtcNow)
              );

        }
    }
}

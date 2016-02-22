namespace ShouldITweetClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Verbotenphrasetextismandatory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VerbotenPhrases", "Phrase", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VerbotenPhrases", "Phrase", c => c.String());
        }
    }
}

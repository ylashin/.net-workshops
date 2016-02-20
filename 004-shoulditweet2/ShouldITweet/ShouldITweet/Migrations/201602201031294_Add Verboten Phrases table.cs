namespace ShouldITweet2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVerbotenPhrasestable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VerbotenPhrases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Phrase = c.String(),
                        LastModified = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VerbotenPhrases");
        }
    }
}

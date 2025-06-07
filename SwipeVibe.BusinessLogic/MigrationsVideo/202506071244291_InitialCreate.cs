namespace SwipeVibe.BusinessLogic.MigrationsVideo
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FileUrl = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        DurationSec = c.Int(nullable: false),
                        LikesCount = c.Int(nullable: false),
                        CommentsCount = c.Int(nullable: false),
                        SharesCount = c.Int(nullable: false),
                        UploadDateUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Videos");
        }
    }
}

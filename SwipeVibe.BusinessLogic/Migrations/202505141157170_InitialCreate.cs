namespace SwipeVibe.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        AvatarUrl = c.String(),
                        ResetPasswordCodeExpiration = c.DateTime(),
                        ResetPasswordCode = c.String(),
                        Role = c.Int(nullable: false),
                        IsBlocked = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        LastLogin = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}

namespace SwipeVibe.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SwipeVibeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SwipeVibeDbContext";
        }

        protected override void Seed(SwipeVibeDbContext context)
        {

        }
    }
}

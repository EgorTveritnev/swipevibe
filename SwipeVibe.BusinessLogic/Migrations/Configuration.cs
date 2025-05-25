namespace SwipeVibe.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SwipeVibe.BusinessLogic.DBModel.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "UserContext";
        }

        protected override void Seed(SwipeVibe.BusinessLogic.DBModel.UserContext context)
        {

        }
    }
}

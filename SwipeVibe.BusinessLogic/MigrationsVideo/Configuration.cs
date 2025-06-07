namespace SwipeVibe.BusinessLogic.MigrationsVideo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SwipeVibe.BusinessLogic.DBModel.VideoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"MigrationsVideo";
            ContextKey = "SwipeVibe.BusinessLogic.DBModel.VideoContext";
        }

        protected override void Seed(SwipeVibe.BusinessLogic.DBModel.VideoContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

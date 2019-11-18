namespace OptimedCorporation.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OptimedCorporation.Models.OptimedCorporationContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
            //ContextKey = "OptimedCorporation.Models.OptimedCorporationContext";

            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "OptimedCorporation.Models.OptimedCorporationContext";
        }

        protected override void Seed(OptimedCorporation.Models.OptimedCorporationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}

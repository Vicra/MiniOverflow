using System.Collections.Generic;
using OverflowVictor.Domain.Entities;

namespace OverflowVictor.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OverflowVictor.Data.OverflowVictorContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OverflowVictor.Data.OverflowVictorContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.Accounts.AddOrUpdate(
            //  p => p.Name,
            //  new Account { Name = "Andrew Peters" },
            //  new Account { Name = "Brice Lambson" },
            //  new Account { Name = "Rowan Miller" }
            //);
            
            
        }
    }
}

namespace M17E_TrabalhoModelo_2021_22.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<M17E_TrabalhoModelo_2021_22.Data.M17E_TrabalhoModelo_2021_22Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(M17E_TrabalhoModelo_2021_22.Data.M17E_TrabalhoModelo_2021_22Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

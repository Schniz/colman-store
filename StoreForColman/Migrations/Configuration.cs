﻿namespace StoreForColman.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StoreForColman.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StoreForColman.Models.ApplicationDbContext context)
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

            context.Users.AddOrUpdate(
                new Models.ApplicationUser
                {
                    UserName = "admin",
                    PasswordHash = "admin",
                    FullName = "המלך התותח",
                    IsAdmin = true,
                    Id = "1",
                }
            );

            context.Products.AddOrUpdate(
                new Models.Product
                {
                    ID = 1,
                    Name = "Pikachu",
                    AmountInStore = 1,
                    ManufactorName = "Pokemon",
                    PriceInNIS = 10.5,
                }
            );
        }
    }
}
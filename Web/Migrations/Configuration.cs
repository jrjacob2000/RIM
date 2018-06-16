namespace Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        

        protected override void Seed(Web.Models.ApplicationDbContext context)
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

            var list = new List<Web.Models.Category> { 
                new Web.Models.Category{ Id = new Guid("FB3C4298-7B27-4434-A2A8-9FC359C5C59D"), Name = "Tube"},
                 new Web.Models.Category{ Id = new Guid("2712E873-7170-4BAB-912C-101593568B59"), Name = "Chain"},
            };


            list.ForEach(d => context.CategoryItems.AddOrUpdate(d));

            var product = new Web.Models.Product()
            {
                Id = new Guid("A3EC0AD4-8FA6-44F1-9C09-A1B64FD042D6"),
                Name = "2.00x17",
                Description = "2.00x17 butyl",
                ReOrderPoint = 0,
                Taxable = false,
                Category = list.FirstOrDefault(),
                Category_Id = list.FirstOrDefault().Id

            };

            context.ProductItems.AddOrUpdate(product);

            var price = new Web.Models.ProductPrice()
            {
                Id = new Guid("31FCA5DF-C540-4AC4-86A7-DDFEA2D8D319"),
                Product = product,
                Product_Id = product.Id,
                PurchasePrice = 5,
                SelesPrice = 6,
                EffectiveFromDate = DateTime.Parse("1/29/2018")
            };

            context.ProductPrices.AddOrUpdate(price);

            context.SaveChanges();
        }
    }
}

namespace FestivalApp.Asp.Net.WebApi.Migrations
{
    using FestivalApp.Asp.Net.WebApi.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FestivalApp.Asp.Net.WebApi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FestivalApp.Asp.Net.WebApi.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Places.AddOrUpdate(
             new Place() { Name = "Novi Sad", Zip = 21000 },
             new Place() { Name = "Guca", Zip = 32230 },
             new Place() { Name = "Beograd", Zip = 11000 },
             new Place() { Name = "Vrnjacka Banja", Zip = 36210 },
             new Place() { Name = "Budapest", Zip = 1007 });
            context.SaveChanges();

            context.Festivals.AddOrUpdate(
             new Festival() { Name = "Exit", TicketPrice = 6000, FirstYear = 2000, PlaceId = 1 },
             new Festival() { Name = "Guca Trumpet Festival", TicketPrice = 2000, FirstYear = 1961, PlaceId = 2 },
             new Festival() { Name = "Sziget", TicketPrice = 8000, FirstYear = 1993, PlaceId = 3 });
            context.SaveChanges();
        }
    }
}

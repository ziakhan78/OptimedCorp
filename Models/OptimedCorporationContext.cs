using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace OptimedCorporation.Models
{
    public class OptimedCorporationContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        //public OptimedCorporationContext() : base("name=OptimedCorporationContext")
        //{
        //    Database.SetInitializer<OptimedCorporationContext>(new CreateDatabaseIfNotExists<OptimedCorporationContext>());
        //   //Database.SetInitializer(new MigrateDatabaseToLatestVersion<OptimedCorporationContext, OptimedCorporation.Migrations.Configuration>());
        //}

        public OptimedCorporationContext()
                : base("name=OptimedCorporationContext")
        { }

        public DbSet<OptimedCorporation.Models.Banner> Banners { get; set; }

        public DbSet<OptimedCorporation.Models.News> News { get; set; }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}

       

        public DbSet<OptimedCorporation.Models.Events> Events { get; set; }

        public DbSet<OptimedCorporation.Models.StoreLocator> StoreLocators { get; set; }

        public DbSet<OptimedCorporation.Models.EventImages> EventImages { get; set; }

        public DbSet<OptimedCorporation.Models.Feedback> Feedback { get; set; }
        public DbSet<OptimedCorporation.Models.Careers> Careers { get; set; }

        public System.Data.Entity.DbSet<OptimedCorporation.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<OptimedCorporation.Models.State> States { get; set; }

        public System.Data.Entity.DbSet<OptimedCorporation.Models.Admin> Admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<OptimedCorporationContext>(null);
            base.OnModelCreating(modelBuilder);
        }
       
    }
}

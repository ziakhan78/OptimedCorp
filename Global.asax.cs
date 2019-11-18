using OptimedCorporation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;


namespace OptimedCorporation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // for web api
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<OptimedCorporationContext>(new DropCreateDatabaseIfModelChanges<OptimedCorporationContext>());

            // Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<Models.OptimedCorporationContext, Migrations.Configuration>());
           // Database.SetInitializer<NameOfDbContext>(new DropCreateDatabaseIfModelChanges<NameOfDbContext>());
           // GlobalFilters.Filters.Add(new System.Web.Http.AuthorizeAttribute()); // for global authorization

        }
    }
}
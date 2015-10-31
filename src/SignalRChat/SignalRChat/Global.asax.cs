using Microsoft.AspNet.SignalR;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SignalRChat
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string connString = ConfigurationManager.ConnectionStrings["ChatConnection"].ConnectionString;
        protected void Application_Start()
        {
            System.Diagnostics.Trace.WriteLine(this.GetType().ToString());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var config = new HubConfiguration();
            //config.EnableDetailedErrors = true;
            //IoCMvcConfig.Initialize();
            //Startup.Configuration(app, config);
            //app.MapSignalR(config);
            //Start SqlDependency with application initialization
            SqlDependency.Start(connString);
        }

        protected void Application_End()
        {
            //Stop SQL dependency
            SqlDependency.Stop(connString);
        }
    }
}

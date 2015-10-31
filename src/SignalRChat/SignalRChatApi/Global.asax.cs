using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;

namespace SignalRChatApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        string connString = ConfigurationManager.ConnectionStrings["ChatConnection"].ConnectionString;
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(IocWebApiConfig.Initialize);
            SqlDependency.Start(connString);
        }

        protected void Application_End()
        {
            SqlDependency.Stop(connString);
        }
    }
}

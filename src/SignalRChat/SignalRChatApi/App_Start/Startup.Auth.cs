using Autofac.Integration.WebApi;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChatApi
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            AutofacWebApiDependencyResolver resolver = System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver as AutofacWebApiDependencyResolver;

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/Token"),
                Provider = resolver.GetService(typeof(SignalRChatApi.Providers.ApplicationOAuthProvider)) as SignalRChatApi.Providers.ApplicationOAuthProvider,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),              
                AllowInsecureHttp = true
            };

            var oauthBearerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(oauthBearerOptions);
        }
    }
}
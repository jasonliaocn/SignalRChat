using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SignalRChat
{
    public class AuthorizationFilterAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            IAuthenticationManager authenticationManager = filterContext.HttpContext.GetOwinContext().Authentication;
            var urlHelper = new UrlHelper(filterContext.RequestContext);
            if (authenticationManager == null || !authenticationManager.User.Identity.IsAuthenticated)
            {
                var url = urlHelper.Action("Login", "Account");
                filterContext.Result = new RedirectResult(url);
            }
            if (authenticationManager.User.Identity.IsAuthenticated)
            {
                var identity = (System.Security.Claims.ClaimsIdentity)authenticationManager.User.Identity;
                if(!identity.HasClaim(ClaimTypes.Role,"AdminUser"))
                {
                    throw new InvalidOperationException("You Are Not Authorized to this");
                }   
            }
        }
    }
}
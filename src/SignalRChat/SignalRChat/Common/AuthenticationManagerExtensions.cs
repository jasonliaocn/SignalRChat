using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SignalRChat.Common.ApiClient;
using SignalRChat.Models;
using System.Security.Claims;

namespace SignalRChat.Common
{
    public static class AuthenticationManagerExtensions
    {
        public static void OwinSignIn(this IAuthenticationManager authenticationManager, UserViewModel user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserDisplayName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            //var roleJson = Newtonsoft.Json.JsonConvert.SerializeObject(user.Roles);
            identity.AddClaim(new Claim(ClaimTypes.Role, user.UserType == 0 ? "AdminUser" : "ChatUser"));
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //CookieHelper.SetCookie<string>(ApplicationConstants.COOKIE_STORENUMBER, ApplicationConstants.SELECT_DEFAULT);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
        }

        public static void OwinSignOut(this IAuthenticationManager authenticationManager)
        {
            //CookieHelper.SetCookie<string>(ApplicationConstants.COOKIE_STORENUMBER, ApplicationConstants.SELECT_DEFAULT);
            CookieHelper.ClearCookie(ApplicationConstants.COOKIE_TOKEN);
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
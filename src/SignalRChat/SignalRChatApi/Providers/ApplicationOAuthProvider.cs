using Microsoft.Owin.Security.OAuth;
using SignalRChatApi.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalRChat.Common;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace SignalRChatApi.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserBO userBO;

        public ApplicationOAuthProvider(IUserBO userBO)
        {
            this.userBO = userBO;
        }

        public override System.Threading.Tasks.Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string userName = context.UserName;
            string password = context.Password;

            var userDto = userBO.GetUser(userName);
            if (userDto == null)
            {
                context.SetError("InvalidGrant", "The user you entered is incorrect.");
            }
            else
            {
                if (password.MD5EncryptToString() != userDto.Password)
                {
                    context.SetError("InvalidGrant", "The password you entered is incorrect.");
                }
                else
                {
                    string userJson = Newtonsoft.Json.JsonConvert.SerializeObject(userDto);

                    var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                    oAuthIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userDto.UserId.ToString()));
                    oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, userDto.UserName));
                    oAuthIdentity.AddClaim(new Claim(ClaimTypes.Role, userDto.UserType > 0 ? "ChatUser" : "AdminUser"));

                    var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {"user",userJson},
                    });

                    var ticket = new AuthenticationTicket(oAuthIdentity, props);
                    context.Validated(ticket);
                    return base.GrantResourceOwnerCredentials(context);
                }
            }
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }
            //if (context.ClientId == null)
            //{
            //    context.SetError("invalid_clientId", "client_Id is not set");
            //    return Task.FromResult<object>(null);
            //}
            //var userDto = userBO.GetUser(int.Parse(context.ClientId));

            //if (userDto == null)
            //{
            //    context.SetError("invalid_userId", string.Format("Invalid user_id '{0}'", context.ClientId));
            //    return Task.FromResult<object>(null);
            //}

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}
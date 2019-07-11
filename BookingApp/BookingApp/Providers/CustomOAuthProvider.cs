using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BookingApp.Models;

namespace BookingApp.Providers
{
    public class CustomOAuthProvider : Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            ApplicationUserManager userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            BAIdentityUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.!!!!");
                return;
            }

            DBContext db = new DBContext();
            var userRole = user.Roles.FirstOrDefault();
            var role = db.Roles.SingleOrDefault(r => r.Id == userRole.RoleId);
            var roleName = role?.Name;
            if(roleName=="Admin")
            {
                context.OwinContext.Response.Headers.Add("Role", new[] { "Admin" });
            }
            else if(roleName=="User")
            {
                context.OwinContext.Response.Headers.Add("Role", new[] { "User" });
            }


            context.OwinContext.Response.Headers.Add("Access-Control-Expose-Headers", new[] { "Role", "user_id" });
            context.OwinContext.Response.Headers.Add("user_id", new[] { user.Id }); 


         /*   AppUser appUser = new AppUser();
            var userId = user.AppUserId;

            if(userId.Equals(appUser.Id))
            {
                context.OwinContext.Response.Headers.Add("Id", new[] { "userID" });
            }


            //if (!user.EmailConfirmed)
            //{
            //    context.SetError("invalid_grant", "AppUser did not confirm email.");
            //    return;
            //}
            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim("sub", context.UserName));
            //identity.AddClaim(new Claim("role", "user"));*/
            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, "JWT");

            var ticket = new AuthenticationTicket(oAuthIdentity, null);

            context.Validated(ticket);

        }
    }
}
 
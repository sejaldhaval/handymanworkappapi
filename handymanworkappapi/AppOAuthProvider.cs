using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using System.Security.Claims;
using handymanworkappapi.Controllers;

namespace handymanworkappapi
{
    public class AppOAuthProvider: OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //validate the user if it exist in database -- call a method of employee controller with username and password
            EmployeeController user = new EmployeeController();
            _Employee emp = new _Employee {
                Email = context.UserName,
                Password = context.Password
            };
            bool validateduser = user.ValidateUser(emp);

            if (validateduser) {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("Email", context.UserName));
                identity.AddClaim(new Claim("Password", context.Password));
                context.Validated(identity);
            }
        }
    }
}
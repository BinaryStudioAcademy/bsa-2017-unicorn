using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;

namespace Unicorn.Core.Providers
{
    public class MembershipProvider : IMembershipProvider
    {
        public Task<ClaimsIdentity> GetUserClaims(string provider, string uid)
        {
            string login = "Test";
            string role = "Admin";

            var claims = new List<Claim>
                {
                    new Claim("login", login),
                    new Claim("role", role),
                    new Claim("somedata_bla_bla", "Make .NET great again!")
                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return Task.FromResult(claimsIdentity);
        }

        public Task<bool> VerifyUser(string provider, string uid)
        {
            /* ТУДУ:
             
            AccountSocialLogin result;

            switch(provider)
            {
                case "facebook":
                    result = db.AccountSocialLogin.FindAsync(x => x.FacebookUID == uid);
                    break;
                case "google":
                    result = db.AccountSocialLogin.FindAsync(x => x.GoogleUID == uid);
                    break;
                case default:
                    result = null;
                    break;
            }
            
            return result == null ? false : true;


             */

            return Task.FromResult(true);

            // TODO: Get data from DB
            //throw new NotImplementedException();
        }
    }
}

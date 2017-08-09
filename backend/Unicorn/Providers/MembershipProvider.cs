using System.Collections.Generic;
using System.Security.Claims;

namespace Unicorn.Providers
{
    public class MembershipProvider
    {
        public ClaimsIdentity GetUserClaims(string provider, string uid)
        {
            // TODO: Get from DB            
            var account = 123;
            string login = "Test", role = "Admin"; // Read data from account

            if (account != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // if user not found
            return null;
        }

        public bool VerifyUser(string provider, string uid)
        {
            // TODO: Get from DB
            if (provider == "facebook" && uid == "123456")
                return true;
            return false;
        }
    }
}
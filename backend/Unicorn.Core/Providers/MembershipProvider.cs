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
            // TODO: Get data from DB
            throw new NotImplementedException();
        }

        public Task<bool> VerifyUser(string provider, string uid)
        {
            // TODO: Get data from DB
            throw new NotImplementedException();
        }
    }
}

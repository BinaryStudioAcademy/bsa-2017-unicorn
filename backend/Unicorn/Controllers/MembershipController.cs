using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Unicorn.Core.Interfaces;
using Unicorn.Core.Providers;
using Unicorn.Core.Services;

namespace Unicorn.Controllers
{
    public class MembershipController : ApiController
    {
        private IAuthService authService;

        /*
        public MembershipController(IAuthService authService)
        {
            this.authService = new AuthJWTService(new MembershipProvider());
        }
        */

        // GET: Membership
        [HttpHead]
        public async Task<string> Get(string provider, string uid)
        {
            authService = new AuthJWTService(new MembershipProvider());

            if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(uid))
            {
                return null; // TODO: throw 404 or smth
            }           

            string token = await authService.GenerateJwtTokenAsync(provider, uid);

            return token; // ?? or try to store in header

        }
    }
}
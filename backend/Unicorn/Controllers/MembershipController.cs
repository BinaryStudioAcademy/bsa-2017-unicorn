using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Unicorn.Core.Interfaces;

namespace Unicorn.Controllers
{
    public class MembershipController : ApiController
    {
        private readonly IAuthService authService;

        public MembershipController(IAuthService authService)
        {
            this.authService = authService;
        }

        // GET: Membership
        [HttpGet]
        public async Task Authenticate(string provider, string uid)
        {
            if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(uid))
            {
                return; // TODO: throw 404 or smth
            }           

            string token = await authService.GenerateJwtTokenAsync(provider, uid);

            // return token ?? or try to store in header

        }
    }
}
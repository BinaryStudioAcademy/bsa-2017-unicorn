using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Unicorn.Services;

namespace Unicorn.Controllers
{
    public class MembershipController : ApiController
    {
        private readonly AuthService _authService;

        public MembershipController()
        {
            _authService = new AuthService(new Providers.MembershipProvider());
        }

        // GET: Membership
        public string Get(string provider, string uid)
        {
            string Token = _authService.GenerateJwtToken(provider, uid);
            return Token;
        }

        public string Post([FromBody]string token)
        {
            return _authService.ValidateToken(token).ToString();
        }
    }
}
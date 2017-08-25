using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;

namespace Unicorn.Controllers
{
    [RoutePrefix("account")]
    [EnableCors("*", "*", "*")]
    public class AccountController : ApiController
    {
        public AccountController(
            IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetProfileById(long id)
        {
            var result = await _accountService.GetProfileInfoAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        private IAccountService _accountService;
    }
}

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
    [RoutePrefix("switch")]
    [EnableCors("*", "*", "*")]
    public class SwitchController : ApiController
    {
        private readonly IAuthService _authService;

        public SwitchController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id)
        {
            HttpResponseMessage response = null;

            string token = await _authService.GenerateToken(id);

            if (token == null)
            {
                // Special status code for registration
                response = Request.CreateResponse(HttpStatusCode.NoContent, "Uid not found");
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Expose-Headers", "Token");
            response.Headers.Add("Token", token);

            return response;
        }

    }
}

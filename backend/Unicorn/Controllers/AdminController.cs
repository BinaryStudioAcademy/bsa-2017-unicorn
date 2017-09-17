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
    [RoutePrefix("admin")]
    [EnableCors("*", "*", "*")]
    public class AdminController : ApiController
    {
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        [HttpGet]
        [Route("auth")]
        public async Task<HttpResponseMessage> ValidateLogin(string login, string password)
        {
            string token = await _adminService.ValidateLogin(login, password);

            if (token == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Expose-Headers", "Token");
            response.Headers.Add("Token", token);

            return response;
        }

        private readonly IAdminService _adminService;
    }
}

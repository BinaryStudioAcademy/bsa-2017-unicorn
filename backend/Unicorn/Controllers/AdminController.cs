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
            if (_adminService.ValidateLogin(login, password))
                return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK));
            else
                return await Task.Run(() => Request.CreateResponse(HttpStatusCode.NotFound));
        }

        private readonly IAdminService _adminService;
    }
}

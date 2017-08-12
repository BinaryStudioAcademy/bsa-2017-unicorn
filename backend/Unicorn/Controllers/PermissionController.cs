using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Unicorn.Core.Interfaces;

namespace Unicorn.Controllers
{
    public class PermissionController : ApiController
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        // GET: permission/
        [HttpGet]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var result = await _permissionService.GetAllAsync();
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

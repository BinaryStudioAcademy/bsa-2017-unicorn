using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Unicorn.Shared.DTOs;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.Controllers
{
    public class RolesController : ApiController
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: roles/1
        [ResponseType(typeof(RoleDTO))]
        [Route("roles/{uid}")]
        public async Task<IHttpActionResult> GetRole(long uid)
        {
            var result = await _roleService.GetByUserId(uid);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

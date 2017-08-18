using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        private ICustomerService _customerService;

        public UserController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/User/5
        [Route("user/{id}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var result = await _customerService.GetById(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }
    }
}

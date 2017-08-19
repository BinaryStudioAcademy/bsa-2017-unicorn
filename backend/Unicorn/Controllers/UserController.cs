using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.User;

namespace Unicorn.Controllers
{
    [RoutePrefix("users")]
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        private ICustomerService _customerService;

        public UserController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var result = await _customerService.GetById(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<HttpResponseMessage> UpdateCustomerAsync(long id, [FromBody]UserShortDTO userDTO)
        {
            await _customerService.UpdateCustomerAsync(userDTO);

            var result = await _customerService.GetById(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}

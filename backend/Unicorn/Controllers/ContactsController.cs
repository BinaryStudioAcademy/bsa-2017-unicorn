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
    [RoutePrefix("contacts")]
    [EnableCors("*", "*", "*")]
    public class ContactsController : ApiController
    {
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        [Route("providers")]
        public async Task<HttpResponseMessage> GetAllProviders()
        {
            var result = await _contactService.GetAllProvidersAsync();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        private IContactService _contactService;
    }
}

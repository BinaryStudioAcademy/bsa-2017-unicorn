using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Context;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    public class TestController : ApiController
    {
        private readonly ILocationService _locationService;
        private readonly IAccountService _acc;

        public TestController(ILocationService locationService, IAccountService acc)
        {
            Database.SetInitializer<AppContext>(new UnicornDbInitializer());
            _locationService = locationService;
            _acc = acc;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var result = await _acc.GetAllAsync();
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllAsync(int id)
        {
            var result = await _acc.GetById(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

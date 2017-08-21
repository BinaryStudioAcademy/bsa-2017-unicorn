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
    [RoutePrefix("work")]
    [EnableCors("*", "*", "*")]
    public class WorkController : ApiController
    {
        public WorkController(IWorkService workService)
        {
            _workService = workService;
        }

        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var result = await _workService.GetAllAsync();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        private IWorkService _workService;
    }
}

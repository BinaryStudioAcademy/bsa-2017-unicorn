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
    [RoutePrefix("categories")]
    [EnableCors("*", "*", "*")]
    public class CategoriesController : ApiController
    {
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var result = await _categoryService.GetAllAsync();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        private ICategoryService _categoryService;
    }
}

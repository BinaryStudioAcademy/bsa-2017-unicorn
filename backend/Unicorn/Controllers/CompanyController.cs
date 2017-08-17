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
    [EnableCors("*", "*", "*")]
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: /Company
        [Route("api/company")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _companyService.GetAllCompaniesAsync();
            if (result != null)
                return Json(result);
            return NotFound();
        }


        // GET: api/Company/5
        [Route("api/company/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _companyService.GetCompanyByIdAsync(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }

        // POST: api/Company
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Company/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Company/5
        public void Delete(int id)
        {
        }
    }
}

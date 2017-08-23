using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.Company;
using Unicorn.Shared.DTOs.CompanyPage;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CompanyController : ApiController
    {
        private readonly ICompanyPageService _companyService;
        private readonly IRatingService _ratingService;

        public CompanyController(ICompanyPageService companyService, IRatingService ratingService)
        {
            _companyService = companyService;
            _ratingService = ratingService;
        }

        // GET: /company
        [HttpGet]
        [Route("company")]
        public async Task<IHttpActionResult> GetAllCompanies()
        {
            var result = await _companyService.GetAllCompanies();
            if (result != null)
                return Json(result);
            return NotFound();
        }

        // GET: /company-short
        [HttpGet]
        [Route("company-short/{id}")]
        public async Task<IHttpActionResult> GetCompanyShort(long id)
        {
            var result = await _companyService.GetCompanyShort(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }


        // GET: company-details/5
        [HttpGet]
        [Route("company-details/{id}")]
        public async Task<IHttpActionResult> GetCompanyDetails(int id)
        {
            var result = await _companyService.GetCompanyDetails(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }

        // POST: company-details
        [HttpPost]
        [Route("company-details")]
        public async Task PostCompanyDetails([FromBody]CompanyDetails company)
        {
            await _companyService.SaveCompanyDetails(company);
        }

        // GET: company-reviews/5
        [HttpGet]
        [Route("company-reviews/{id}")]
        public async Task<IHttpActionResult> GetCompanyReviews(int id)
        {
            var result = await _companyService.GetCompanyReviews(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }

        // POST: company-reviews
        [HttpPost]
        [Route("company-reviews")]
        public async Task PostCompanyReviews([FromBody]CompanyReviews company)
        {
            await _companyService.SaveCompanyReviews(company);
        }

        // GET: company-vendors/5
        [HttpGet]
        [Route("company-vendors/{id}")]
        public async Task<IHttpActionResult> GetCompanyVendors(int id)
        {
            var result = await _companyService.GetCompanyVendors(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }

        // POST: company-vendors
        [HttpPost]
        [Route("company-vendors")]
        public async Task PostCompanyVendors([FromBody]CompanyVendors company)
        {
            await _companyService.SaveCompanyVendors(company);
        }

        // GET: company-contacts/5
        [HttpGet]
        [Route("company-contacts/{id}")]
        public async Task<IHttpActionResult> GetCompanyContacts(int id)
        {
            var result = await _companyService.GetCompanyContacts(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }

        // POST: company-contacts
        [HttpPost]
        [Route("company-contacts")]
        public async Task PostCompanyContacts([FromBody]CompanyContacts company)
        {
            await _companyService.SaveCompanyContacts(company);
        }

        // GET: company-works/5
        [HttpGet]
        [Route("company-works/{id}")]
        public async Task<IHttpActionResult> GetCompanyWorks(int id)
        {
            var result = await _companyService.GetCompanyWorks(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }
        [HttpGet]
        [Route("company/{id}/rating")]
        public async Task<HttpResponseMessage> GetCompanyRating(long id)
        {
            var accountId = await _companyService.GetCompanyAccountId(id);
            var result = await _ratingService.GetAvarageByRecieverId(accountId);
            return Request.CreateResponse(HttpStatusCode.OK, result);

        // POST: company-works
        [HttpPost]
        [Route("company-works")]
        public async Task PostCompanyWorks([FromBody]CompanyWorks company)
        {
            await _companyService.SaveCompanyWorks(company);
        }

        // GET: company-books/5
        [HttpGet]
        [Route("company-books/{id}")]
        public async Task<IHttpActionResult> GetCompanyBooks(int id)
        {
            var result = await _companyService.GetCompanyBooks(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }

        // POST: company-books
        [HttpPost]
        [Route("company-books")]
        public async Task PostCompanyBooks([FromBody]CompanyBooks company)
        {
            await _companyService.SaveCompanyBooks(company);
        }

        //// PUT: api/Company/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Company/5
        //public void Delete(int id)
        //{
        //}
    }
}

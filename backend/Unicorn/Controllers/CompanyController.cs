using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Filters;
using Unicorn.Shared.DTOs.Chart;
using Unicorn.Shared.DTOs.CompanyPage;
using Unicorn.Shared.DTOs.Contact;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CompanyController : ApiController
    {
        private readonly ICompanyPageService _companyService;
        private readonly IRatingService _ratingService;
        private readonly IAnalyticsService _analytics;

        public CompanyController(ICompanyPageService companyService, IRatingService ratingService, IAnalyticsService analytics)
        {
            _companyService = companyService;
            _ratingService = ratingService;
            _analytics = analytics;
        }

        // GET: /company
        [HttpGet]
        [Route("company")]
        public async Task<IHttpActionResult> GetAllCompanies()
        {
            var result = await _companyService.GetAllCompanies();

            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        // GET: company/short
        [HttpGet]
        [Route("company/short/{id}")]
        public async Task<IHttpActionResult> GetCompanyShort(long id)
        {
            var result = await _companyService.GetCompanyShort(id);

            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        // GET: company/details/5
        [HttpGet]
        [Route("company/details/{id}")]
        public async Task<IHttpActionResult> GetCompanyDetails(long id)
        {
            var result = await _companyService.GetCompanyDetails(id);

            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        // POST: company/details
        [HttpPost]
        [Route("company/details")]
        [TokenAuthenticate]
        public async Task SaveCompanyDetails([FromBody]CompanyDetails companyDetails) => await _companyService.SaveCompanyDetails(companyDetails);

        // GET: company/reviews/5
        [HttpGet]
        [Route("company/reviews/{id}")]
        public async Task<IHttpActionResult> GetCompanyReviews(long id)
        {
            var result = await _companyService.GetCompanyReviews(id);

            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        // PUT: company/reviews
        [HttpPut]
        [Route("company/reviews")]
        [TokenAuthenticate]
        public async Task AddCompanyReviews([FromBody]CompanyReviews companyReviews) => await _companyService.AddCompanyReviews(companyReviews);

        // GET: company/dashboard/vendors/5
        [HttpGet]
        [Route("company/{id}/dashboard/vendors")]
        public async Task<List<VendorDTO>> GetCompanyVendorsWithWorks(long id) => await _companyService.GetCompanyVendorsWithWorks(id);

        // GET: company/vendors/5
        [HttpGet]
        [Route("company/vendors/{id}")]
        public async Task<IHttpActionResult> GetCompanyVendors(long id)
        {
            var result = await _companyService.GetCompanyVendors(id);

            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        // PUT: company/vendors
        [HttpPut]
        [Route("company/vendors")]
        [TokenAuthenticate]
        public async Task AddCompanyVendors([FromBody]CompanyVendors companyVendors) => await _companyService.AddCompanyVendors(companyVendors);

        // DELETE: company/vendor/5/4
        [HttpDelete]
        [Route("company/vendor/{companyId}/{vendorId}")]
        [TokenAuthenticate]
        public async Task DeleteCompanyVendor(long companyId, long vendorId) => await _companyService.DeleteCompanyVendor(companyId, vendorId);

        // GET: company/contacts/5
        [HttpGet]
        [Route("company/contacts/{id}")]
        public async Task<IHttpActionResult> GetCompanyContacts(long id)
        {
            var result = await _companyService.GetCompanyContacts(id);

            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        // POST: company/contacts
        [HttpPost]
        [Route("company/contact")]
        [TokenAuthenticate]
        public async Task SaveCompanyContact([FromBody]ContactShortDTO companyContact) => await _companyService.SaveCompanyContact(companyContact);

        // PUT: company/contact/5
        [HttpPut]
        [Route("company/contact/{companyId}")]
        [TokenAuthenticate]
        public async Task AddCompanyContact(long companyId, [FromBody]ContactShortDTO companyContact) => await _companyService.AddCompanyContact(companyId, companyContact);

        // DELETE: company/contact/5
        [HttpDelete]
        [Route("company/contact/{companyId}/{contactId}")]
        [TokenAuthenticate]
        public async Task DeleteCompanyContact(long companyId, long contactId) => await _companyService.DeleteCompanyContact(companyId, contactId);

        // GET: company/works/5
        [HttpGet]
        [Route("company/works/{id}")]
        public async Task<IHttpActionResult> GetCompanyWorks(long id)
        {
            var result = await _companyService.GetCompanyWorks(id);

            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        // POST: company/work
        [HttpPost]
        [Route("company/work")]
        [TokenAuthenticate]
        public async Task SaveCompanyWork([FromBody]CompanyWork companyWork) => await _companyService.SaveCompanyWork(companyWork);

        // PUT: company/work/5
        [HttpPut]
        [Route("company/work/{companyId}")]
        [TokenAuthenticate]
        public async Task AddCompanyWork(long companyId, [FromBody]CompanyWork companyWork) => await _companyService.AddCompanyWork(companyId, companyWork);

        // DELETE: company/work/5
        [HttpDelete]
        [Route("company/work/{companyId}/{workId}")]
        [TokenAuthenticate]
        public async Task DeleteCompanyWork(long companyId, long workId) => await _companyService.DeleteCompanyWork(companyId, workId);

        // GET: company/books/5
        [HttpGet]
        [Route("company/books/{id}")]
        public async Task<IHttpActionResult> GetCompanyBooks(long id)
        {
            var result = await _companyService.GetCompanyBooks(id);

            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        // POST: company/book
        [HttpPost]
        [Route("company/book")]
        [TokenAuthenticate]
        public async Task SaveCompanyBook([FromBody]CompanyBook companyBook) => await _companyService.SaveCompanyBook(companyBook);

        // GET: company/1/rating
        [HttpGet]
        [Route("company/{id}/rating")]
        public async Task<HttpResponseMessage> GetCompanyRating(long id)
        {
            var accountId = await _companyService.GetCompanyAccountId(id);
            var result = await _ratingService.GetAvarageByRecieverId(accountId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: company/1/charts
        [HttpGet]
        [Route("company/{id}/charts")]
        [TokenAuthenticate]
        public async Task<AnalyticsDTO> GetCompanyCharts(long id) => await _analytics.GetCompanyAnalytics(id);
    }
}

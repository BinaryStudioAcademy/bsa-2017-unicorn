using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.Chart;
using Unicorn.Shared.DTOs.Company;
using Unicorn.Shared.DTOs.CompanyPage;
using Unicorn.Shared.DTOs.Contact;

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
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("company")]
        public async Task<IHttpActionResult> GetAllCompanies()
        {
            var result = await _companyService.GetAllCompanies();
            if (result != null)
                return Json(result);
            return NotFound();
        }



        // GET: company/short
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("company/short/{id}")]
        public async Task<IHttpActionResult> GetCompanyShort(long id)
        {
            var result = await _companyService.GetCompanyShort(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }
        // GET: company/details/5
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("company/details/{id}")]
        public async Task<IHttpActionResult> GetCompanyDetails(long id)
        {
            var result = await _companyService.GetCompanyDetails(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }
        // POST: company/details
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("company/details")]
        public async Task SaveCompanyDetails([FromBody]CompanyDetails companyDetails)
        {
            await _companyService.SaveCompanyDetails(companyDetails);
        }



        // GET: company/reviews/5
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("company/reviews/{id}")]
        public async Task<IHttpActionResult> GetCompanyReviews(long id)
        {
            var result = await _companyService.GetCompanyReviews(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }
        // PUT: company/reviews
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("company/reviews")]
        public async Task AddCompanyReviews([FromBody]CompanyReviews companyReviews)
        {
            await _companyService.AddCompanyReviews(companyReviews);
        }



        // GET: company/vendors/5
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("company/vendors/{id}")]
        public async Task<IHttpActionResult> GetCompanyVendors(long id)
        {
            var result = await _companyService.GetCompanyVendors(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }
        // PUT: company/vendors
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("company/vendors")]
        public async Task AddCompanyVendors([FromBody]CompanyVendors companyVendors)
        {
            await _companyService.AddCompanyVendors(companyVendors);
        }
        // DELETE: company/vendor/5/4
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("company/vendor/{companyId}/{vendorId}")]
        public async Task DeleteCompanyVendor(long companyId, long vendorId)
        {
            await _companyService.DeleteCompanyVendor(companyId, vendorId);
        }
        


        // GET: company/contacts/5
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("company/contacts/{id}")]
        public async Task<IHttpActionResult> GetCompanyContacts(long id)
        {
            var result = await _companyService.GetCompanyContacts(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }
        // POST: company/contacts
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("company/contact")]
        public async Task SaveCompanyContact([FromBody]ContactShortDTO companyContact)
        {
            await _companyService.SaveCompanyContact(companyContact);
        }
        // PUT: company/contact/5
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("company/contact/{companyId}")]
        public async Task AddCompanyContact(long companyId, [FromBody]ContactShortDTO companyContact)
        {
            await _companyService.AddCompanyContact(companyId, companyContact);
        }
        // DELETE: company/contact/5
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("company/contact/{companyId}/{contactId}")]
        public async Task DeleteCompanyContact(long companyId, long contactId)
        {
            await _companyService.DeleteCompanyContact(companyId, contactId);
        }



        // GET: company/works/5
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("company/works/{id}")]
        public async Task<IHttpActionResult> GetCompanyWorks(long id)
        {
            var result = await _companyService.GetCompanyWorks(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }
        // POST: company/work
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("company/work")]
        public async Task SaveCompanyWork([FromBody]CompanyWork companyWork)
        {
            await _companyService.SaveCompanyWork(companyWork);
        }
        // PUT: company/work/5
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("company/work/{companyId}")]
        public async Task AddCompanyWork(long companyId, [FromBody]CompanyWork companyWork)
        {
            await _companyService.AddCompanyWork(companyId, companyWork);
        }
        // DELETE: company/work/5
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("company/work/{companyId}/{workId}")]
        public async Task DeleteCompanyWork(long companyId, long workId)
        {
            await _companyService.DeleteCompanyWork(companyId, workId);
        }



        // GET: company/books/5
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("company/books/{id}")]
        public async Task<IHttpActionResult> GetCompanyBooks(long id)
        {
            var result = await _companyService.GetCompanyBooks(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }
        // POST: company/book
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("company/book")]
        public async Task SaveCompanyBook([FromBody]CompanyBook companyBook)
        {
            await _companyService.SaveCompanyBook(companyBook);
        }



        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("company/{id}/rating")]
        public async Task<HttpResponseMessage> GetCompanyRating(long id)
        {
            var accountId = await _companyService.GetCompanyAccountId(id);
            var result = await _ratingService.GetAvarageByRecieverId(accountId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("company/{id}/charts")]
        public async Task<AnalyticsDTO> GetCompanyCharts(long id)
        {
            return await _analytics.GetCompanyAnalytics(id);
        }
    }
}

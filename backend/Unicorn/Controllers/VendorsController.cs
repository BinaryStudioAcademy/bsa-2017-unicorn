using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.Subcategory;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Controllers
{
    [RoutePrefix("vendors")]
    [EnableCors("*", "*", "*")]
    public class VendorsController : ApiController
    {
        public VendorsController(IVendorService vendorService, IReviewService reviewService, IPortfolioService portfolioService)
        {
            _vendorService = vendorService;
            _reviewService = reviewService;
            _portfolioService = portfolioService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var result = await _vendorService.GetAllAsync();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetById(long id)
        {
            var result = await _vendorService.GetByIdAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<HttpResponseMessage> UpdateVendor(long id, [FromBody]ShortVendorDTO vendor)
        {
            await _vendorService.Update(vendor);

            var result = await _vendorService.GetByIdAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("{id}/categories")]
        public async Task<HttpResponseMessage> GetVendorCategories(long id)
        {
            var result = await _vendorService.GetVendorCategoriesAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("{id}/contacts")]
        public async Task<HttpResponseMessage> GetVendorContacts(long id)
        {
            var result = await _vendorService.GetVendorContacts(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("{id}/reviews")]
        public async Task<HttpResponseMessage> GetVendorReviews(long id)
        {
            var result = await _reviewService.GetByReceiverIdAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("{id}/rating")]
        public async Task<HttpResponseMessage> GetVendorRating(long id)
        {
            var accountId = await _vendorService.GetVendorAccountIdAsync(id);
            var result = await _reviewService.GetReceiverRatingAsync(accountId);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        [HttpGet]
        [Route("{id}/portfolio")]
        public async Task<HttpResponseMessage> GetVendorPortfolio(long id)
        {
            var result = await _portfolioService.GetItemsByVendorIdAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        IVendorService _vendorService;
        IReviewService _reviewService;
        IPortfolioService _portfolioService;
    }
}

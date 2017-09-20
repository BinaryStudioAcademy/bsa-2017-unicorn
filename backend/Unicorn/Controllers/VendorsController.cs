using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.Filters;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Book;
using Unicorn.Shared.DTOs.Chart;
using Unicorn.Shared.DTOs.Contact;
using Unicorn.Shared.DTOs.Subcategory;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Controllers
{
    [RoutePrefix("vendors")]
    [EnableCors("*", "*", "*")]
    public class VendorsController : ApiController
    {
        public VendorsController(
            IVendorService vendorService, 
            IReviewService reviewService, 
            IPortfolioService portfolioService, 
            IBookService bookService,
            IHistoryService historyService,
            IWorkService workService,
            IContactService contactService,
            IRatingService ratingService,
            INotificationProxy notificationProxy,
            IAnalyticsService analyticsService)
        {
            _vendorService = vendorService;
            _reviewService = reviewService;
            _portfolioService = portfolioService;
            _bookService = bookService;
            _historyService = historyService;
            _workService = workService;
            _contactService = contactService;
            _ratingService = ratingService;
            _notificationProxy = notificationProxy;
            _analyticsService = analyticsService;
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
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> UpdateVendor(long id, [FromBody]ShortVendorDTO vendor)
        {
            await _vendorService.UpdateAsync(vendor);

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
        [Route("{id}/charts")]
        [TokenAuthenticate]
        public async Task<AnalyticsDTO> GetVendorCharts(long id)
        {
            return await _analyticsService.GetVendorAnalytics(id);
        }

        [HttpGet]
        [Route("{id}/works")]
        public async Task<HttpResponseMessage> GetVendorWorks(long id)
        {
            var result = await _vendorService.GetVendorWorksAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("{id}/works")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> CreateVendorWork(long id, [FromBody]WorkDTO workDto)
        {
            workDto.VendorId = id;
            var result = await _workService.CreateAsync(workDto);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [HttpPut]
        [Route("{id}/works/{workId}")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> UpdateVendorWork(long id, long workId, [FromBody]WorkDTO workDto)
        {
            await _workService.UpdateAsync(workDto);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{id}/works/{workId}")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> UpdateVendorWork(long id, long workId)
        {
            await _workService.RemoveByIdAsync(workId);

            var result = await _vendorService.GetVendorWorksAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("{id}/history")]
        public async Task<HttpResponseMessage> GetVendorHistory(long id)
        {
            var result = await _historyService.GetVendorHistoryAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("{id}/orders")]
        public async Task<HttpResponseMessage> GetVendorOrders(long id)
        {
            var result = await _bookService.GetOrdersAsync("vendor", id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("{id}/orders/{orderId}")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> UpdateVendor(long id, long orderId, [FromBody]VendorBookDTO order)
        {
            var book = await _bookService.GetByIdAsync(orderId);
            book.Status = order.Status;
            await _bookService.Update(book);

            var result = await _bookService.GetOrdersAsync("vendor", id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("{id}/contacts")]
        public async Task<HttpResponseMessage> GetVendorContacts(long id)
        {
            var result = await _vendorService.GetVendorContactsAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("{id}/contacts")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> PostVendorContact(long id, [FromBody]ContactShortDTO contact)
        {
            var accountId = await _vendorService.GetVendorAccountIdAsync(id);
            var result = await _contactService.CreateAsync(accountId, contact);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("{id}/contacts/{contactId}")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> PutVendorContact(long id, long contactId, [FromBody]ContactShortDTO contact)
        {
            var accountId = await _vendorService.GetVendorAccountIdAsync(id);
            await _contactService.UpdateAsync(accountId, contact);

            var result = await _contactService.GetByIdAsync(contactId);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("{id}/contacts/{contactId}")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> DeleteVendorContact(long id, long contactId)
        {
            var accountId = await _vendorService.GetVendorAccountIdAsync(id);
            await _contactService.RemoveAsync(accountId, contactId);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("{id}/reviews")]
        public async Task<HttpResponseMessage> GetVendorReviews(long id)
        {
            var accountId = await _vendorService.GetVendorAccountIdAsync(id);
            var result = await _reviewService.GetByReceiverIdAsync(accountId);

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
            var result = await _ratingService.GetAvarageByRecieverId(accountId);
            return Request.CreateResponse(HttpStatusCode.OK, result);

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
        
        [HttpPost]
        [Route("{id}/portfolio")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> GetVendorPortfolio(long id, [FromBody] PortfolioItemDTO itemDto)
        {
            await _portfolioService.CreateAsync(id, itemDto);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        private IVendorService _vendorService;
        private IReviewService _reviewService;
        private IPortfolioService _portfolioService;
        private IBookService _bookService;
        private IHistoryService _historyService;
        private IRatingService _ratingService;
        private IWorkService _workService;
        private IContactService _contactService;
        private INotificationProxy _notificationProxy;
        private IAnalyticsService _analyticsService;
    }
}

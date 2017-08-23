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
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Book;
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
            IContactService contactService)
        {
            _vendorService = vendorService;
            _reviewService = reviewService;
            _portfolioService = portfolioService;
            _bookService = bookService;
            _historyService = historyService;
            _workService = workService;
            _contactService = contactService;
        }

        #region Get

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
        [Route("{id}/works")]
        public async Task<HttpResponseMessage> GetVendorWorks(long id)
        {
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
            var result = await _bookService.GetVendorOrdersAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

#region Contacts

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
        public async Task<HttpResponseMessage> PostVendorContact(long id, [FromBody]ContactShortDTO contact)
        {
            var accountId = await _vendorService.GetVendorAccountIdAsync(id);
            await _contactService.CreateAsync(accountId, contact);

            var result = await _vendorService.GetVendorContactsAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("{id}/contacts/{contactId}")]
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
        public async Task<HttpResponseMessage> DeleteVendorContact(long id, long contactId)
        {
            var accountId = await _vendorService.GetVendorAccountIdAsync(id);
            await _contactService.RemoveAsync(accountId, contactId);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        #endregion

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
#endregion
        
        #region Post

        [HttpPost]
        [Route("{id}/portfolio")]
        public async Task<HttpResponseMessage> GetVendorPortfolio(long id, [FromBody] PortfolioItemDTO itemDto)
        {
            await _portfolioService.CreateAsync(id, itemDto);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpPost]
        [Route("{id}/works")]
        public async Task<HttpResponseMessage> CreateVendorWork(long id, [FromBody]WorkDTO workDto)
        {
            workDto.VendorId = id;
            await _workService.CreateAsync(workDto);

            var result = await _vendorService.GetVendorWorksAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        #endregion

        #region Put

        [HttpPut]
        [Route("{id}")]
        public async Task<HttpResponseMessage> UpdateVendor(long id, [FromBody]ShortVendorDTO vendor)
        {
            await _vendorService.UpdateAsync(vendor);

            var result = await _vendorService.GetByIdAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("{id}/orders/{orderId}")]
        public async Task<HttpResponseMessage> UpdateVendor(long id, long orderId, [FromBody]VendorBookDTO order)
        {
            var book = await _bookService.GetByIdAsync(orderId);
            book.Status = order.Status;
            await _bookService.Update(book);

            var result = await _bookService.GetVendorOrdersAsync(orderId);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpPut]
        [Route("{id}/works/{workId}")]
        public async Task<HttpResponseMessage> UpdateVendorWork(long id, long workId, [FromBody]WorkDTO workDto)
        {
            await _workService.UpdateAsync(workDto);

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        #endregion

        #region Delete

        [HttpDelete]
        [Route("{id}/works/{workId}")]
        public async Task<HttpResponseMessage> UpdateVendorWork(long id, long workId)
        {
            await _workService.RemoveByIdAsync(workId);

            var result = await _vendorService.GetVendorWorksAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion

        private IVendorService _vendorService;
        private IReviewService _reviewService;
        private IPortfolioService _portfolioService;
        private IBookService _bookService;
        private IHistoryService _historyService;
        private IWorkService _workService;
        private IContactService _contactService;
    }
}

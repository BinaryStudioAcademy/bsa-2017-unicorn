using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.Book;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    public class BookController : ApiController
    {
        private IBookService _bookService;
        private INotificationService _notificationService;

        public BookController(IBookService bookService, INotificationService notificationService)
        {
            _bookService = bookService;
            _notificationService = notificationService;
        }

        [HttpPost]
        [Route("{id}/order")]
        public async Task<HttpResponseMessage> Order(BookOrderDTO book)
        {
            try
            {
                await _bookService.Create(book);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("book/{role}/{id}/pending")]
        public async Task<HttpResponseMessage> GetPendingBooks(string role, long id)
        {
            IEnumerable<VendorBookDTO> books;
            try
            {
                books = await _bookService.GetPendingOrdersAsync(role, id);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(books);
        }

        [HttpGet]
        [Route("book/{role}/{id}/accepted")]
        public async Task<HttpResponseMessage> GetVendorBooks(string role, long id)
        {
            IEnumerable<VendorBookDTO> books;
            try
            {
                books = await _bookService.GetAcceptedOrdersAsync(role, id);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(books);
        }

        [HttpGet]
        [Route("book/{role}/{id}/finished")]
        public async Task<HttpResponseMessage> GetFinishedBooks(string role, long id)
        {
            IEnumerable<VendorBookDTO> books;
            try
            {
                books = await _bookService.GetFinishedOrdersAsync(role, id);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(books);
        }

        [HttpPut]
        [Route("book")]
        public async Task<HttpResponseMessage> UpdateBook(VendorBookDTO book)
        {
            try
            {
                await _bookService.Update(book);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("book/customer/{id}")]
        public async Task<HttpResponseMessage> GetCustomerBooks(long id)
        {
            IEnumerable<CustomerBookDTO> books;
            try
            {
                books = await _bookService.GetCustomerBooks(id);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(HttpStatusCode.OK, books);
        }

        [HttpDelete]
        [Route("book/{id}")]
        public async Task<HttpResponseMessage> DeleteBook(long id)
        {
            try
            {
                await _bookService.DeleteBook(id);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

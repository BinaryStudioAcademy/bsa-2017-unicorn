using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Filters;
using Unicorn.Shared.DTOs.User;

namespace Unicorn.Controllers
{
    [RoutePrefix("users")]
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        private ICustomerService _customerService;
        private IReviewService _reviewService;
        private IRatingService _ratingService;

        public UserController(ICustomerService customerService, IReviewService reviewService, IRatingService ratingService)
        {
            _customerService = customerService;
            _reviewService = reviewService;
            _ratingService = ratingService;
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var result = await _customerService.GetById(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }

        [HttpGet]
        [Route("{id}/order")]
        public async Task<IHttpActionResult> GetForOrder(long id)
        {
            var result = await _customerService.GetForOrderAsync(id);
            if (result != null)
                return Json(result);
            return NotFound();
        }

        [HttpPut]
        [Route("{id}")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> UpdateCustomerAsync(long id, [FromBody]UserShortDTO userDTO)
        {
            await _customerService.UpdateCustomerAsync(userDTO);

            var result = await _customerService.GetById(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpGet]
        [Route("{id}/reviews")]
        public async Task<HttpResponseMessage> GetUserReviews(long id)
        {
            var result = await _reviewService.GetByReceiverIdAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("{id}/ratings")]
        public async Task<HttpResponseMessage> GetAllUserRatings(long id)
        {
            var accountId = await _customerService.GetUserAccountIdAsync(id);
            var result = await _ratingService.GetByReceiverIdAsync(accountId);

            
            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        [HttpGet]
        [Route("{id}/rating")]
        public async Task<HttpResponseMessage> GetUserRating(long id)
        {
            var accountId = await _customerService.GetUserAccountIdAsync(id);
            var result = await _ratingService.GetAvarageByRecieverId(accountId);
                 return Request.CreateResponse(HttpStatusCode.OK, result);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.Review;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ReviewController : ApiController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        [Route("review")]
        public async Task<HttpResponseMessage> SaveReview(ShortReviewDTO review)
        {
            try
            {
                await _reviewService.SaveReview(review);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

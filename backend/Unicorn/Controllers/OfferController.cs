using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Unicorn.Shared.DTOs.Offer;

namespace Unicorn.Controllers
{
    public class OfferController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> SendOffersAsync(ShortOfferDTO offer)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("offer/{id}")]
        public async Task<IHttpActionResult> GetOffersAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
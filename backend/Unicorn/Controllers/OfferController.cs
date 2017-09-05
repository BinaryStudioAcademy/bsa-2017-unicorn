using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.Offer;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    public class OfferController : ApiController
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpPost]
        [Route("offer")]
        public async Task<HttpResponseMessage> SendOffersAsync(IEnumerable<ShortOfferDTO> offers)
        {
            try
            {
                await _offerService.CreateOffersAsync(offers);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("offer/vendor/{id}")]
        public async Task<HttpResponseMessage> GetVendorOffersAsync(long id)
        {
            try
            {
                var offers = await _offerService.GetVendorOffersAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, offers);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("offer/company/{id}")]
        public async Task<HttpResponseMessage> GetCompanyOffersAsync(long id)
        {
            try
            {
                var offers = await _offerService.GetCompanyOffersAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, offers);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [Route("offer")]
        public async Task<HttpResponseMessage> UpdateOfferAsync(OfferDTO offer)
        {
            try
            {
                await _offerService.UpdateOfferAsync(offer);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("offer/{id}")]
        public async Task<HttpResponseMessage> DeleteOfferAsync(long id)
        {
            try
            {
                await _offerService.DeleteOfferAsync(id);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
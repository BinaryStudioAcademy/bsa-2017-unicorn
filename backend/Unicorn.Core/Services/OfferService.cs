using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs.Offer;

namespace Unicorn.Core.Services
{
    class OfferService : IOfferService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OfferService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task CreateOffersAsync(ShortOfferDTO offer)
        {


            throw new NotImplementedException();
        }

        public Task<OfferDTO> GetOffersAsync(long vendorId)
        {
            throw new NotImplementedException();
        }
    }
}

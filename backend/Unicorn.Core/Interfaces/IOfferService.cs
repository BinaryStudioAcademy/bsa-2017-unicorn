using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Offer;

namespace Unicorn.Core.Interfaces
{
    interface IOfferService
    {
        Task CreateOffersAsync(ShortOfferDTO offer);
        Task<OfferDTO> GetOffersAsync(long vendorId);
    }
}

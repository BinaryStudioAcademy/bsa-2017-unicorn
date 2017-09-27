using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Offer;

namespace Unicorn.Core.Interfaces
{
    public interface IOfferService
    {
        Task CreateOffersAsync(IEnumerable<ShortOfferDTO> offer);
        Task<IEnumerable<OfferDTO>> GetVendorOffersAsync(long vendorId);
        Task<IEnumerable<OfferDTO>> GetCompanyOffersAsync(long vendorId);
        Task UpdateOfferAsync(OfferDTO offer);
        Task DeleteOfferAsync(long id);
    }
}

using Unicorn.DataAccess.Entities.Enum;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Shared.DTOs.Offer
{
    public class OfferDTO
    {
        public long Id { get; set; }

        public string AttachedMessage { get; set; }

        public string DeclinedMessage { get; set; }

        public OfferStatus Status { get; set; }

        public CompanyDTO Company { get; set; }

        public VendorDTO Vendor { get; set; }
    }
}

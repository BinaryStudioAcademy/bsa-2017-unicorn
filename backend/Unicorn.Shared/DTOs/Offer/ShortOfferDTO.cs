using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.DTOs.Offer
{
    public class ShortOfferDTO
    {
        public long VendorId { get; set; }

        public long CompanyId { get; set; }

        public string AttachedMessage { get; set; }
    }
}

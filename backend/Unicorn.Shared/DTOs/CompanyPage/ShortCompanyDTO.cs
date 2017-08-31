using System;
using System.Collections.Generic;
using Unicorn.Shared.DTOs.Contact;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Shared.DTOs.Company
{
    public class ShortCompanyDTO
    {
        public long Id { get; set; }

        public long AccountId { get; set; }

        public string Avatar { get; set; }

        public string Name { get; set; }

        public LocationDTO Location { get; set; }
    }
}
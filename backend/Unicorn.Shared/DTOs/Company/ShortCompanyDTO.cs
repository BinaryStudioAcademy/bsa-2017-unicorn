using System;
using System.Collections.Generic;
using Unicorn.Shared.DTOs.Contact;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Shared.DTOs.Company
{
    public class ShortCompanyDTO
    {
        public long Id { get; set; }

        public string Avatar { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime FoundationDate { get; set; }

        public string Director { get; set; }

        public LocationDTO Location { get; set; }

        public ICollection<VendorDTO> Vendors { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; }

        public ICollection<ContactShortDTO> Contacts { get; set; }
    }
}
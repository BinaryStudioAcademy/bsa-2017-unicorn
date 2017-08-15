using System;
using System.Collections.Generic;

namespace Unicorn.Shared.DTOs
{
    public class CompanyDTO
    {
        public long Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime FoundationDate { get; set; }

        public AccountDTO Director { get; set; }

        public int Staff { get; set; }

        public AccountDTO Account { get; set; }

        public LocationDTO Location { get; set; }

        public ICollection<VendorDTO> Vendors { get; set; }
    }
}

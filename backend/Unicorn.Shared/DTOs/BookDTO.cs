using System;

namespace Unicorn.Core.DTOs
{
    public class BookDTO
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }


        public CustomerDTO Customer { get; set; }

        public VendorDTO Vendor { get; set; }

        public CompanyDTO Company { get; set; }

        public WorkDTO Work { get; set; }

        public LocationDTO Location { get; set; }
    }
}

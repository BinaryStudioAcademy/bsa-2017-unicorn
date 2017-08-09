using System;

namespace Unicorn.Shared.DTOs
{
    public class BookDTO
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }


        public virtual CustomerDTO Customer { get; set; }

        public virtual VendorDTO Vendor { get; set; }

        public virtual WorkDTO Work { get; set; }

        public virtual LocationDTO Location { get; set; }
    }
}

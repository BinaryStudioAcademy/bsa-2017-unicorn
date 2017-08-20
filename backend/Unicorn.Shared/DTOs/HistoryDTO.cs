using System;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Shared.DTOs
{
    public class HistoryDTO
    {
        public long Id { get; set; }

        public CustomerDTO Customer { get; set; }

        public ShortVendorDTO Vendor { get; set; }

        public ReviewDTO Review { get; set; }

        public CompanyDTO Company { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateFinished { get; set; }

        public string BookDescription { get; set; }

        public string WorkDescription { get; set; }

        public string CategoryName { get; set; }

        public string SubcategoryName { get; set; }
    }
}

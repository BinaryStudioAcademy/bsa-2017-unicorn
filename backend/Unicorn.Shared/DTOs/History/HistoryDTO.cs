using System;

using Unicorn.Shared.DTOs.Review;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Shared.DTOs.History
{
    public class HistoryDTO
    {
        public long Id { get; set; }

        public CustomerDTO Customer { get; set; }

        public ShortVendorDTO Vendor { get; set; }

        public ReviewDTO Review { get; set; }

        public CompanyDTO Company { get; set; }

        public DateTimeOffset Date { get; set; }

        public DateTimeOffset DateFinished { get; set; }

        public string BookDescription { get; set; }

        public string WorkDescription { get; set; }

        public string CategoryName { get; set; }

        public string SubcategoryName { get; set; }
    }
}

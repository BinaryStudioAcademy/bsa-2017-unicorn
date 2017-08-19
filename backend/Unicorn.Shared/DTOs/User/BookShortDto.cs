using System;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Shared.DTOs.User
{
    public class BookShortDto
    {
        public DateTime date { get; set; }
        public string address { get; set; }
        public string workType { get; set; }
        public string description { get; set; }
        public VendorDTO vendor { get; set; }
        public string status { get; set; }
    }
}

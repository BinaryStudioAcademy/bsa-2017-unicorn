using System;

namespace Unicorn.Shared.DTOs.User
{
    public class BookShortDto
    {
        public DateTime Date { get; set; }
        public string Adress { get; set; }
        public string workType { get; set; }
        public string Description { get; set; }
        public VendorShortDto Vendor { get; set; }
        public string Sratus { get; set; }
    }
}

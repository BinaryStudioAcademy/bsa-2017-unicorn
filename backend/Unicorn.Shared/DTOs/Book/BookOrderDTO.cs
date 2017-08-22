using System;

namespace Unicorn.Shared.DTOs.Book
{
    public class BookOrderDTO
    {
        public LocationDTO Adress { get; set; }
        public DateTime Date { get; set; }
        public long CustomerId { get; set; }
        public long CompanyId { get; set; }
        public string WorkType { get; set; } // work id?
        public string Description { get; set; }
        public long VendorId { get; set; }        
    }
}

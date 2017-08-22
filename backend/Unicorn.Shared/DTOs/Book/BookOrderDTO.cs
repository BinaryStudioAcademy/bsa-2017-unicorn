using System;

namespace Unicorn.Shared.DTOs.Book
{
    public class BookOrderDTO
    {
        public LocationDTO Adress { get; set; }
        public long CustomerId { get; set; }
        public long CompanyId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Profile { get; set; }
        public long ProfileId { get; set; }
        public long VendorId { get; set; }        
        public long WorkId { get; set; }
    }
}

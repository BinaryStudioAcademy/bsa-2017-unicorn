using System;

using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Shared.DTOs.Book
{
    public class BookDTO
    {
        public long Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public BookStatus Status { get; set; }

        public string Description { get; set; }

        public string DeclinedReason { get; set; }

        public long ParentBookId { get; set; }

        public bool IsCompanyTask { get; set; }

        public CustomerDTO Customer { get; set; }

        public VendorDTO Vendor { get; set; }

        public CompanyDTO Company { get; set; }

        public WorkDTO Work { get; set; }

        public LocationDTO Location { get; set; }
    }
}

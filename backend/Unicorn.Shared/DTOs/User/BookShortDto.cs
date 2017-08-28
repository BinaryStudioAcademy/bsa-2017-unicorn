using System;
using Unicorn.Shared.DTOs.Vendor;

using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Entities.Enum;

namespace Unicorn.Shared.DTOs.User
{
    public class BookShortDto
    {
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public string WorkType { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public BookStatus Status { get; set; }
    }
}

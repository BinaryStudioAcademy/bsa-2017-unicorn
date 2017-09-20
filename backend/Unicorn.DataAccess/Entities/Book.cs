using System;

using Unicorn.DataAccess.Entities.Enum;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Book : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset Date { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public BookStatus Status { get; set; }

        public string Description { get; set; }

        public virtual Customer Customer { get; set; }

        public string CustomerPhone { get; set; }

        public string DeclinedReason { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual Company Company { get; set; } 

        public virtual Work Work { get; set; }

        public virtual Location Location { get; set; }

        public bool IsHidden { get; set; }

        public bool IsCompanyTask { get; set; }

        public long ParentBookId { get; set; }
    }
}
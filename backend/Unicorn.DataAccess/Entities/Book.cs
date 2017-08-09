using System;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Book : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual Work Work { get; set; }

        public virtual Location Location { get; set; }
    }
}
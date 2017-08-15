using System;
using System.Collections.Generic;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Company : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime FoundationDate { get; set; }

        public int Staff { get; set; }

        public virtual Account Account { get; set; }

        public virtual Location Location { get; set; }

        public virtual IEnumerable<Vendor> Vendors { get; set; }
    }
}
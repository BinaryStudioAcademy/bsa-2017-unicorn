using System;
using System.Collections.Generic;

namespace Unicorn.DataAccess.Entities
{
    public class Company
    {
        public long Id { get; set; }

        public DateTime FoundationDate { get; set; }

        public int Staff { get; set; }


        public virtual Account Account { get; set; }

        public virtual Location Location { get; set; }

        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
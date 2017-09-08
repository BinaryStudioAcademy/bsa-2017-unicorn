using System;
using System.Collections.Generic;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Company : IEntity
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime FoundationDate { get; set; }

        public string Director { get; set; }
        
        public int Staff { get; set; }

        public string DirectorContact { get; set; }

        public virtual Account Account { get; set; }

        public virtual Calendar Calendar { get; set; }
       
        public virtual ICollection<Vendor> Vendors { get; set; }

        public virtual ICollection<Work> Works { get; set; }

    }
}
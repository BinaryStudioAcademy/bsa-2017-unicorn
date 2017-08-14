using System.Collections.Generic;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Work : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual Subcategory Subcategory { get; set; }

        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
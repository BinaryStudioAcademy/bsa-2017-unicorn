using System.Collections.Generic;

namespace Unicorn.DataAccess.Entities
{
    public class Work
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public virtual Subcategory Subcategory { get; set; }

        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
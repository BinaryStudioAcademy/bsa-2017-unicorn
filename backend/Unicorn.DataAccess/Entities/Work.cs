using System.Collections.Generic;

using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Work : IEntity
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Name { get; set; }

        public long Orders { get; set; }

        public string Description { get; set; }

        public virtual Subcategory Subcategory { get; set; }

        public string Icon { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual Company Company { get; set; }
    }
}
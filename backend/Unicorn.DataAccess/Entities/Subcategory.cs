using System.Collections.Generic;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Subcategory : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Icon { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Work> Works { get; set; }
    }
}

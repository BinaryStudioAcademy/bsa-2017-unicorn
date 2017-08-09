using System.Collections.Generic;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Subcategory : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<Work> Works { get; set; }
    }
}

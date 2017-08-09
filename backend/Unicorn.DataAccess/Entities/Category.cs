using System.Collections.Generic;

namespace Unicorn.DataAccess.Entities
{
    public class Category
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}

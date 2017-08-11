using System.Collections.Generic;

namespace Unicorn.Core.DTOs
{
    public class CategoryDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<SubcategoryDTO> Subcategories { get; set; }
    }
}

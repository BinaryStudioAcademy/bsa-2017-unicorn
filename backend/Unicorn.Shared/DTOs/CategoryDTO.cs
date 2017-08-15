using System.Collections.Generic;

namespace Unicorn.Shared.DTOs
{
    public class CategoryDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<SubcategoryDTO> Subcategories { get; set; }
    }
}

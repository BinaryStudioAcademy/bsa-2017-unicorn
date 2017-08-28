using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.Subcategory
{
    public class SubcategoryDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public virtual CategoryDTO Category { get; set; }

        public virtual ICollection<WorkDTO> Works { get; set; }
    }
}

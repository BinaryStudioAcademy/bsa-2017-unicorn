using System.Collections.Generic;

namespace Unicorn.Shared.DTOs
{
    public class SubcategoryDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public CategoryDTO Category { get; set; }

        public ICollection<WorkDTO> Works { get; set; }
    }
}
